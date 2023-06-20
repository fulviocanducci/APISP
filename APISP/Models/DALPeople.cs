using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Reflection;

namespace APISP.Models
{
   public sealed class DALPeople
   {
      private readonly SqlConnection _connection;

      public DALPeople(SqlConnection connection)
      {
         _connection = connection;
      }

      public async Task<People> AddAsync(People model)
      {
         _connection.Open();
         using SqlCommand command = _connection.CreateCommand();
         command.CommandType = System.Data.CommandType.StoredProcedure;
         command.CommandText = "PEOPLE_ADD";
         command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50).Value = model.Name;
         command.Parameters.Add("@Active", System.Data.SqlDbType.Bit).Value = model.Active;
         using (SqlDataReader reader = await command.ExecuteReaderAsync())
         {
            if (reader is not null && reader.HasRows && reader.Read())
            {
               model.Id = (int)reader["Id"];
            }
         }
         _connection.Close();
         return model;
      }
      public async Task<bool> UpdateAsync(People model)
      {
         bool status = false;
         _connection.Open();
         using SqlCommand command = _connection.CreateCommand();
         command.CommandType = System.Data.CommandType.StoredProcedure;
         command.CommandText = "PEOPLE_UPDATE";
         command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50).Value = model.Name;
         command.Parameters.Add("@Active", System.Data.SqlDbType.Bit).Value = model.Active;
         command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = model.Id;
         status = await command.ExecuteNonQueryAsync() > 0;
         _connection.Close();
         return status;
      }

      public async Task<People?> FindAsync(int id)
      {
         People? model = null;
         _connection.Open();
         using SqlCommand command = _connection.CreateCommand();
         command.CommandType = System.Data.CommandType.StoredProcedure;
         command.CommandText = "PEOPLE_FIND";
         command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
         using (SqlDataReader reader = await command.ExecuteReaderAsync())
         {
            if (reader is not null && reader.HasRows)
            {
               model = new People();
               while (await reader.ReadAsync())
               {
                  model.Id = (int)reader["Id"];
                  model.Name = reader["Name"].ToString();
                  model.Active = (bool)reader["Active"];
               }
            }
         }
         _connection.Close();
         return model;
      }

      public async IAsyncEnumerable<People> FindAllAsync()
      {
         _connection.Open();
         using SqlCommand command = _connection.CreateCommand();
         command.CommandType = System.Data.CommandType.StoredProcedure;
         command.CommandText = "PEOPLE_FINDALL";
         using (SqlDataReader reader = await command.ExecuteReaderAsync())
         {
            if (reader is not null && reader.HasRows)
            {
               while (await reader.ReadAsync())
               {
                  yield return new People()
                  {
                     Id = (int)reader["Id"],
                     Name = reader["Name"].ToString(),
                     Active = (bool)reader["Active"]
                  };
               }
            }
         }
         _connection.Close();
      }

      public async Task<bool> DeleteAsync(int id)
      {
         bool status = false;
         _connection.Open();
         using SqlCommand command = _connection.CreateCommand();
         command.CommandType = System.Data.CommandType.StoredProcedure;
         command.CommandText = "PEOPLE_DELETE";
         command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
         status = await command.ExecuteNonQueryAsync() > 0;
         _connection.Close();
         return status;
      }
   }
}
