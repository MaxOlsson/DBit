/*
       The MIT License (MIT)

        Copyright (c) 2013, Max Olsson

        Permission is hereby granted, free of charge, to any person obtaining a copy
        of this software and associated documentation files (the "Software"), to deal
        in the Software without restriction, including without limitation the rights
        to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        copies of the Software, and to permit persons to whom the Software is
        furnished to do so, subject to the following conditions:

        The above copyright notice and this permission notice shall be included in
        all copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
        AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
        THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBitNET
{
    public class DBit : IDisposable
    {
        public DBit() { }
        public DBit(SqlConnection Connection)
        {
            this.Connection = Connection;
        }

        public DBit(string Connection)
        {
            this.Connection = new SqlConnection(Connection);
        }

        public SqlCommand Command { get; set; }
        public SqlConnection Connection { get; set; }

        public DBit StoredProcedure(string procedure)
        {
            if (string.IsNullOrWhiteSpace(procedure)) { throw new ArgumentException("The parameter 'procedure' cannot be null"); }
            if (this.Connection == null) { throw new NullReferenceException("The database connection cannot be null"); }

            this.Command = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = procedure,
                Connection = this.Connection
            };

            return this;
        }
        public DBit Query(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) { throw new ArgumentException("The parameter 'procedure' cannot be null"); }
            if (this.Connection == null) { throw new NullReferenceException("The database connection cannot be null"); }

            this.Command = new SqlCommand()
            {
                CommandType = CommandType.Text,
                CommandText = query,
                Connection = this.Connection
            };

            return this;
        }

        public DBit AddParameter(string paramName, string value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.String,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, Guid value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Guid,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, short value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Int16,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, int value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Int32,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, long value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Int64,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, bool value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Boolean,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, DateTime value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.DateTime,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, double value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Double,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, decimal value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Decimal,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, byte value, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Byte,
                Direction = paramDirection
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, string[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.Object,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }
        public DBit AddParameter(string paramName, Guid[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.Guid,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }
        public DBit AddParameter(string paramName, short[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.Int16,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }
        public DBit AddParameter(string paramName, int[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }
        public DBit AddParameter(string paramName, long[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.Int64,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }
        public DBit AddParameter(string paramName, bool[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.Boolean,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }
        public DBit AddParameter(string paramName, DateTime[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.DateTime,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }
        public DBit AddParameter(string paramName, double[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.Double,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }
        public DBit AddParameter(string paramName, decimal[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.Decimal,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }
        public DBit AddParameter(string paramName, byte[] args)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, len = args.Length; i < len; i++)
            {
                builder.Append(paramName + i);
                if (i < (len - 1)) { builder.Append(","); }
            }

            this.Command.CommandText = this.Command.CommandText.Replace(paramName, builder.ToString());

            for (int i = 0, len = args.Length; i < len; i++)
            {
                SqlParameter param = new SqlParameter(paramName + i, args[i])
                {
                    DbType = DbType.Byte,
                    Direction = ParameterDirection.InputOutput
                };
                this.Command.Parameters.Add(param);
            }

            return this;
        }

        public int Execute()
        {
            int result = -1;
            try
            {
                if (this.Command.Connection.State != ConnectionState.Open)
                {
                    this.Command.Connection.Open();
                }

                result = this.Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Command.Connection.Close();
            }
            return result;
        }
        public DataSet ExecuteDataSet()
        {
            DataSet ds = null;
            try
            {
                if (this.Command.Connection.State != ConnectionState.Open)
                {
                    this.Command.Connection.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(this.Command);
                ds = new DataSet();
                da.Fill(ds);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Command.Connection.Close();
            }
            return ds;
        }
        public T Populate<T>()
        {
            T instance = (T)Activator.CreateInstance(typeof(T));

            try
            {
                if (this.Command.Connection.State != ConnectionState.Open)
                {
                    this.Command.Connection.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(this.Command);
                DataSet ds = new DataSet();
                da.Fill(ds);

                instance = Populate<T>(ds.Tables[0].Rows[0]);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Command.Connection.Close();
            }

            return instance;
        }
        public dynamic Populate()
        {
            try
            {
                if (this.Command.Connection.State != ConnectionState.Open)
                {
                    this.Command.Connection.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(this.Command);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return Populate(ds.Tables[0].Rows[0]);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Command.Connection.Close();
            }
        }
        public IEnumerable<T> PopulateModels<T>()
        {
            try
            {
                if (this.Command.Connection.State != ConnectionState.Open)
                {
                    this.Command.Connection.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(this.Command);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return PopulateModels<T>(ds.Tables[0]);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Command.Connection.Close();
            }
        }
        public IEnumerable<dynamic> PopulateModels()
        {
            try
            {
                if (this.Command.Connection.State != ConnectionState.Open)
                {
                    this.Command.Connection.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(this.Command);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return PopulateModels(ds.Tables[0]);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Command.Connection.Close();
            }
        }

        public static dynamic Populate(DataRow row)
        {
            return new DBObject(row);
        }
        public static T Populate<T>(DataRow row)
        {
            T instance = (T)Activator.CreateInstance(typeof(T));

            if (typeof(T).IsPrimitive || typeof(T).Name == "String")
            {
                return (T)row[0];
            }

            PropertyInfo[] pinfo = instance.GetType().GetProperties();

            for (int i = 0; i < pinfo.Count(); i++)
            {
                PropertyInfo Property = pinfo[i];
                for (int j = 0; j < row.Table.Columns.Count; j++)
                {
                    DataColumn Column = row.Table.Columns[j];
                    if (Property.Name.ToLower() == Column.ColumnName.ToLower() && Property.PropertyType.Equals(Column.DataType))
                    {
                        object value = row[Column.ColumnName].Equals(DBNull.Value) ? null : row[Column.ColumnName];
                        instance.GetType().GetProperty(Property.Name).SetValue(instance, value, null);
                    }
                }
            }

            return instance;
        }
        public static IEnumerable<T> PopulateModels<T>(DataTable table)
        {
            List<T> models = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                models.Add(Populate<T>(row));
            }
            return models;
        }
        public static IEnumerable<dynamic> PopulateModels(DataTable table)
        {
            List<dynamic> models = new List<dynamic>();
            foreach (DataRow row in table.Rows)
            {
                models.Add(new DBObject(row));
            }
            return models;
        }

        public void Dispose()
        {
            if (this.Command != null)
            {
                this.Command.Cancel();

                if (this.Command.Connection != null)
                {
                    this.Command.Connection.Close();
                    this.Command.Connection.Dispose();
                }

                this.Command.Dispose();
            }
        }
    }

    //Keep this class within the same file to make it easy to copy/paste this API if it's ever needed or preferred
    public class DBObject : DynamicObject
    {
        private readonly DataRow _row;

        internal DBObject(DataRow row) { _row = row; }

        // Interprets a member-access as an indexer-access on the contained DataRow.
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _row[binder.Name] ?? null;
            return (result != null);
        }
    }
}
