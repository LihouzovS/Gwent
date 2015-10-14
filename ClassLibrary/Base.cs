using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.SqlClient;
namespace ClassLibrary
{
    public class Base<T> where T : Base<T>
    {
        private static SqlConnection Connection = new SqlConnection();
        [SaveAttribute]
        public Guid ID { get; set; }

        public Base()
        {
            ID = Guid.NewGuid();
        }

        public static void ConnectToDatabase()
        {
            string ConnStr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Serg\Documents\GwentDB.mdf;Integrated Security=True;Connect Timeout=30";
              Connection = new SqlConnection(ConnStr);
              Connection.Open();
        }

        public static void Disconnect()
        {
            Connection.Close();
        }

        private void SetParametres(SqlCommand cmd, object Instance)
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
                    cmd.Parameters["@" + p.Name].Value = p.GetValue(Instance);
                }
        }

        public void Save()
        {
            string TableName = typeof(T).Name + "s";
            string Query = "IF OBJECT_ID('" + TableName + "', 'U') IS NULL\n" +
                 "\tCREATE TABLE " + TableName + "\n" + "\t(\n"; ;
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                {
                    Query += "\t\t" + p.Name + " ";
                    if (p.PropertyType == typeof(string))
                        Query += "nvarchar(50),\n";
                    else if (p.PropertyType == typeof(int))
                        Query += "int,\n";
                    else if (p.PropertyType == typeof(bool))
                        Query += "bit,\n";
                    else if (p.PropertyType == typeof(Guid))
                        Query += "uniqueidentifier,\n";
                    else
                        throw new System.Data.SqlTypes.SqlTypeException();
                }
            Query += "\t)\n";
            Query += "INSERT INTO " + TableName + " VALUES (";
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                    Query += "@" + p.Name + ", ";
            Query = Query.Remove(Query.Length - 2);
            Query += ")";
            SqlCommand cmd = new SqlCommand(Query, Connection);
            SetParametres(cmd, this);
            cmd.ExecuteNonQuery();
        }

        static public T FindByField(string field, string value)
        {
            string TableName = typeof(T).Name + "s";
            string Query = "IF OBJECT_ID('" + TableName + "', 'U') IS NOT NULL\n";
            Query += "\tSELECT * FROM " + TableName + " WHERE " + field + "=" + "@value";
            SqlCommand cmd = new SqlCommand(Query, Connection);
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

        public void Update(T NewValue)
        {
            string TableName = typeof(T).Name + "s";
            string Query = "IF OBJECT_ID('" + TableName + "', 'U') IS NOT NULL\n";
            Query += "\tUPDATE " + TableName + " SET ";
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                    Query += p.Name + "=@" + p.Name + ", ";
            Query = Query.Remove(Query.Length - 2);
            Query += "\n";
            Query += "\tWHERE ID = @old_ID";
            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.Add("@old_ID", System.Data.SqlDbType.UniqueIdentifier);
            cmd.Parameters["@old_ID"].Value = ID;
            SetParametres(cmd, NewValue);
            cmd.ExecuteNonQuery();
        }

        public void Delete()
        {
            string TableName = typeof(T).Name + "s";
            string Query = "IF OBJECT_ID('" + TableName + "', 'U') IS NOT NULL\n";
            Query += "\tDELETE FROM " + TableName + " WHERE ";
            foreach (var p in typeof(T).GetProperties())
                if (p.IsDefined(typeof(SaveAttribute)))
                    Query += p.Name + "=@" + p.Name + " AND ";
            Query = Query.Remove(Query.Length - 5);
            SqlCommand cmd = new SqlCommand(Query, Connection);
            SetParametres(cmd, this);
            cmd.ExecuteNonQuery();
        }
    }
}
