/*
 *      Copyright 2013, Max Olsson
 *
 *      Licensed under the Apache License, Version 2.0 (the "License");
 *      you may not use this file except in compliance with the License.
 *      You may obtain a copy of the License at

 *          http://www.apache.org/licenses/LICENSE-2.0

 *      Unless required by applicable law or agreed to in writing, software
 *      distributed under the License is distributed on an "AS IS" BASIS,
 *      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *      See the License for the specific language governing permissions and
 *      limitations under the License.
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;

namespace DBitNET.DBit
{
    public class DBit : IDisposable
    {
        public DBit() { }
        public DBit(SqlConnection Connection)
        {
            this.Connection = Connection;
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

        public DBit AddParameter(string paramName, string value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.String,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, Guid value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Guid,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, short value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Int16,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, int value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Int32,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, long value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Int64,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, bool value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Boolean,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, DateTime value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.DateTime,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, double value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Double,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, decimal value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Decimal,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, byte value)
        {
            SqlParameter param = new SqlParameter(paramName, value)
            {
                DbType = DbType.Byte,
                Direction = ParameterDirection.InputOutput
            };
            this.Command.Parameters.Add(param);

            return this;
        }
        public DBit AddParameter(string paramName, string[] args)
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 0, len = args.Length; i < len; i++)
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
            Type type = typeof(T);

            if (type.IsPrimitive || type.Name == "String")
            {
                return (T)row[0];
            }
            else
            {
                T instance = (T)Activator.CreateInstance(type);

                PropertyInfo[] pinfo = instance.GetType().GetProperties();

                for (int i = 0; i < pinfo.Count(); i++)
                {
                    PropertyInfo Property = pinfo[i];
                    for (int j = 0; j < row.Table.Columns.Count; j++)
                    {
                        DataColumn Column = row.Table.Columns[j];

                        if (Property.Name.Equals(Column.ColumnName, StringComparison.OrdinalIgnoreCase) && Property.PropertyType.Equals(Column.DataType))
                        {
                            object value = row[Column.ColumnName].Equals(DBNull.Value) ? null : row[Column.ColumnName];
                            instance.GetType().GetProperty(Property.Name).SetValue(instance, value);
                        }
                    }

                }
                return instance;
            }
        }
        public static IEnumerable<T> PopulateModels<T>(DataTable table)
        {
            return table.AsEnumerable().Select(row =>
            {
                return Populate<T>(row);
            });
        }
        public static IEnumerable<dynamic> PopulateModels(DataTable table)
        {
            return table.AsEnumerable().Select(row =>
            {
                return new DBObject(row);
            });
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
