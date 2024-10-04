using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BIKERENTALAPI.Entity;
using BIKERENTALAPI.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BIKERENTALAPI.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly string _connectionString;

        public ManagerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Bikes> AddBike(Bikes bike)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Bikes (ID,Title,ImagePath,Regnumber,Brand,Category,Description,Model,IsAvailable) VALUES (@ID,@Title,@ImagePath,@Regnumber,@Brand,@Category,@Description,@Model,@IsAvailable)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", bike.ID);
                    command.Parameters.AddWithValue("@Title", bike.Title);
                    command.Parameters.AddWithValue("@ImagePath", bike.ImagePath);
                    command.Parameters.AddWithValue("@Regnumber", bike.Regnumber);
                    command.Parameters.AddWithValue("@Brand", bike.Brand);
                    command.Parameters.AddWithValue("@Category", bike.Category);
                    command.Parameters.AddWithValue("@Description", bike.Description);
                    command.Parameters.AddWithValue("@Model", bike.Model);
                    command.Parameters.AddWithValue("@IsAvailable", bike.IsAvailable);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }
            return bike;
        }



        public async Task<Bikes> GetBikeById(Guid id)
        {
            Bikes bike = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Bikes WHERE ID = @ID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            bike = new Bikes
                            {
                                ID = reader.GetGuid(reader.GetOrdinal("ID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                ImagePath = reader.GetString(reader.GetOrdinal("ImagePath")),
                                Regnumber = reader.GetInt32(reader.GetOrdinal("Regnumber")),
                                Brand = reader.GetString(reader.GetOrdinal("Brand")),
                                Category = reader.GetString(reader.GetOrdinal("Category")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Model = reader.GetString(reader.GetOrdinal("Model")),
                                IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable")),


                            };
                        }
                        connection.Close();
                    }
                }
            }
            return bike;
        }

        public async Task<List<Bikes>> GetAllBikes()
        {
            var bikes = new List<Bikes>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Bikes";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bikes.Add(new Bikes
                            {
                                ID = reader.GetGuid(reader.GetOrdinal("ID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                ImagePath = reader.GetString(reader.GetOrdinal("ImagePath")),
                                Regnumber = reader.GetInt32(reader.GetOrdinal("Regnumber")),
                                Brand = reader.GetString(reader.GetOrdinal("Brand")),
                                Category = reader.GetString(reader.GetOrdinal("Category")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Model = reader.GetString(reader.GetOrdinal("Model")),
                                IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
                            });
                        }
                        connection.Close();
                    }
                }
            }
            return bikes;
        }

        public async Task<Bikes> DeleteBike(Guid id)
        {
            Bikes bike = null;
            using (var connection = new SqlConnection(_connectionString))
            {
           
                var selectQuery = "SELECT * FROM Bikes WHERE ID = @ID";
                using (var selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            bike = new Bikes
                            {
                                ID = reader.GetGuid(reader.GetOrdinal("ID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                ImagePath = reader.GetString(reader.GetOrdinal("ImagePath")),
                                Regnumber = reader.GetInt32(reader.GetOrdinal("Regnumber")),
                                Brand = reader.GetString(reader.GetOrdinal("Brand")),
                                Category = reader.GetString(reader.GetOrdinal("Category")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Model = reader.GetString(reader.GetOrdinal("Model")),
                                IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
                            };
                        }
                        connection.Close();
                    }
                }

              
                var deleteQuery = "DELETE FROM Bikes WHERE ID = @ID";
                using (var deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    await deleteCommand.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }
            return bike;
        }

        public async Task<Bikes> EditBike(Bikes bike)
        {
            Bikes existingBike = null;
            using (var connection = new SqlConnection(_connectionString))
            {
       
                var selectQuery = "SELECT * FROM Bikes WHERE ID = @ID";
                using (var selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@ID", bike.ID);

                    connection.Open();
                    using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            existingBike = new Bikes
                            {
                                ID = reader.GetGuid(reader.GetOrdinal("ID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                ImagePath = reader.GetString(reader.GetOrdinal("ImagePath")),
                                Regnumber = reader.GetInt32(reader.GetOrdinal("Regnumber")),
                                Brand = reader.GetString(reader.GetOrdinal("Brand")),
                                Category = reader.GetString(reader.GetOrdinal("Category")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Model = reader.GetString(reader.GetOrdinal("Model")),
                                IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
                            };
                        }
                        connection.Close();
                    }
                }

                // If the bike exists, update it
                if (existingBike != null)
                {
                    var updateQuery = "UPDATE Bikes SET Title=@Title,ImagePath=@ImagePath,Regnumber=@Regnumber, Brand = @Brand,  Category = @Category,Description=@Description,Model = @Model,IsAvailable=@IsAvailable WHERE ID = @ID";
                    using (var updateCommand = new SqlCommand(updateQuery, connection))
                    {

                        updateCommand.Parameters.AddWithValue("@ID", bike.ID);
                        updateCommand.Parameters.AddWithValue("@Title", bike.Title);
                        updateCommand.Parameters.AddWithValue("@ImagePath", bike.ImagePath);
                        updateCommand.Parameters.AddWithValue("@Regnumber", bike.Regnumber);
                        updateCommand.Parameters.AddWithValue("@Brand", bike.Brand);
                        updateCommand.Parameters.AddWithValue("@Category", bike.Category);
                        updateCommand.Parameters.AddWithValue("@Description", bike.Description);
                        updateCommand.Parameters.AddWithValue("@Model", bike.Model);
                        updateCommand.Parameters.AddWithValue("@IsAvailable", bike.IsAvailable);

                        connection.Open();
                        await updateCommand.ExecuteNonQueryAsync();
                        connection.Close();
                    }
                }
            }
            return existingBike;
        }
    }
}
