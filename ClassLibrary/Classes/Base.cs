namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Data.SqlClient;
    public class Base<T> where T : Base<T>
    {
        private static SqlConnection Connection = new SqlConnection();
        public Base()
        {
            this.ID = Guid.NewGuid();
        }
        [SaveAttribute]
        public Guid ID { get; set; }

        public static void ConnectToDatabase()
        {
            string connStr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Серг\Documents\GwentDB.mdf;Integrated Security=True;Connect Timeout=30";
              Connection = new SqlConnection(connStr);
              Connection.Open();
        }

        public static void Disconnect()
        {
            Connection.Close();
        }

        private void SetParametres(SqlCommand cmd, object instance)
        {
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                {
                    if (p.PropertyType == typeof(string))
                        cmd.Parameters.Add("@" + p.Name, System.Data.SqlDbType.NVarChar);
                    else if (p.PropertyType == typeof(bool))
                        cmd.Parameters.Add("@" + p.Name, System.Data.SqlDbType.Bit);
                    else if (p.PropertyType == typeof(int))
                        cmd.Parameters.Add("@" + p.Name, System.Data.SqlDbType.Int);
                    else if (p.PropertyType == typeof(Guid))
                        cmd.Parameters.Add("@" + p.Name, System.Data.SqlDbType.UniqueIdentifier);
                    else
                        throw new System.Data.SqlTypes.SqlTypeException();
                    cmd.Parameters["@" + p.Name].Value = p.GetValue(instance);
                }
        }

        public void Save()
        {
            string tableName = typeof(T).Name + "s";
            string query = "IF OBJECT_ID('" + tableName + "', 'U') IS NULL\n" +
            "\tCREATE TABLE " + tableName + "\n" + "\t(\n";
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                {
                    query += "\t\t" + p.Name + " ";
                    if (p.PropertyType == typeof(string))
                        query += "nvarchar(50),\n";
                    else if (p.PropertyType == typeof(int))
                        query += "int,\n";
                    else if (p.PropertyType == typeof(bool))
                        query += "bit,\n";
                    else if (p.PropertyType == typeof(Guid))
                        query += "uniqueidentifier,\n";
                    else
                        throw new System.Data.SqlTypes.SqlTypeException();
                }
            query += "\t)\n";
            query += "INSERT INTO " + tableName;
            query += "(";
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                    query += p.Name + ", ";
            query = query.Remove(query.Length - 2);
            query += ") ";
            query += " VALUES (";
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                    query += "@" + p.Name + ", ";
            query = query.Remove(query.Length - 2);
            query += ")";
            SqlCommand cmd = new SqlCommand(query, Connection);
            this.SetParametres(cmd, this);
            cmd.ExecuteNonQuery();
        }

        public static T FindByField(string field, string value)
        {
            string tableName = typeof(T).Name + "s";
            string query = "IF OBJECT_ID('" + tableName + "', 'U') IS NOT NULL\n";
            query += "\tSELECT * FROM " + tableName + " WHERE " + field + "=" + "@value";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.Add(new SqlParameter("@value", value));
            SqlDataReader reader = cmd.ExecuteReader();
            T res = (T)Activator.CreateInstance(typeof(T));
            while (reader.Read())
            {
                foreach (var p in typeof(T).GetProperties())
                    if (p.IsDefined(typeof(SaveAttribute)))
                    {
                        if (p.PropertyType == typeof(string))
                            p.SetValue(res, reader[p.Name]);
                        else if (p.PropertyType == typeof(bool))
                            p.SetValue(res, bool.Parse(reader[p.Name].ToString()));
                        else if (p.PropertyType == typeof(int))
                            p.SetValue(res, int.Parse(reader[p.Name].ToString()));
                        else if (p.PropertyType == typeof(Guid))
                            p.SetValue(res, Guid.Parse(reader[p.Name].ToString()));
                        else
                            throw new System.Data.SqlTypes.SqlTypeException();
                    }
            }
            reader.Close();
            return res;
        }

        public void Update(T newValue)
        {
            string tableName = typeof(T).Name + "s";
            string query = "IF OBJECT_ID('" + tableName + "', 'U') IS NOT NULL\n";
            query += "\tUPDATE " + tableName + " SET ";
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                    query += p.Name + "=@" + p.Name + ", ";
            query = query.Remove(query.Length - 2);
            query += "\n";
            query += "\tWHERE ID = @old_ID";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.Add("@old_ID", System.Data.SqlDbType.UniqueIdentifier);
            cmd.Parameters["@old_ID"].Value = ID;
            SetParametres(cmd, newValue);
            cmd.ExecuteNonQuery();
        }

        public void Delete()
        {
            string tableName = typeof(T).Name + "s";
            string query = "IF OBJECT_ID('" + tableName + "', 'U') IS NOT NULL\n";
            query += "\tDELETE FROM " + tableName + " WHERE ";
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                    query += p.Name + "=@" + p.Name + " AND ";
            query = query.Remove(query.Length - 5);
            SqlCommand cmd = new SqlCommand(query, Connection);
            SetParametres(cmd, this);
            cmd.ExecuteNonQuery();
        }
    }
}
