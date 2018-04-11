using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace CLTimeTableDB
{
    public class DbRepository
    {
        private const string path = "C:/Users/David/Desktop/David/Programovani/MVCProjects/MVCTimetable/CLTimeTableDB/1_ERRORS/Errors.txt";

        //private const string ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=TimetableDB;Integrated Security=True";

        //private ConnectionEntityDL GetDbConnectionEntity(SqlDataReader reader)
        //{
        //    return new ConnectionEntityDL(reader.GetInt32(reader.GetOrdinal("id")),
        //                           reader.GetInt32(reader.GetOrdinal("departureCityId")),
        //                           reader.GetTimeSpan(reader.GetOrdinal("departureTime")),
        //                           reader.GetInt32(reader.GetOrdinal("arrivalCityId")),
        //                           reader.GetTimeSpan(reader.GetOrdinal("arrivalTime"))
        //                          );
        //}

        private void ExceptionLog (string error)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(string));
                using (StreamWriter writer = new StreamWriter(path))
                {
                    serializer.Serialize(writer, error);
                }
            }
            catch (Exception ex)
            {
                string IOError = ex.Message.ToString();
            }
        }
        // GET ALL CONNECTIONS
        public List<ConnectionEntityDL> GetConnections()
        {
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    return accessToDB.ConnectionEntityDLTable.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex.Message.ToString());
                List<ConnectionEntityDL> nullObject = new List<ConnectionEntityDL>();
                nullObject.Add(new ConnectionEntityDL());
                return nullObject;
            }
        }
        //public IEnumerable<ConnectionEntityDL> GetConnections()
        //{
        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT id,departureCityId,arrivalCityId,departureTime,arrivalTime FROM Departures2", connection))
        //        {
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    yield return GetDbConnectionEntity(reader);
        //                }
        //            }
        //        }
        //    }
        //}

        // GET ONE CONNECTION BY ID
        public ConnectionEntityDL GetConnectionByID(int idConnection)
        {
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    return accessToDB.ConnectionEntityDLTable.Where(x => x.Id == idConnection).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex.Message.ToString());
                ConnectionEntityDL nullObject = new ConnectionEntityDL();string errorMessage = $"{ex.Message.ToString()}";
                return nullObject;
            }
            
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("SELECT id,departureCityId,arrivalCityId,departureTime,arrivalTime FROM Departures2 WHERE id=@idConnection ", connection))
            //    {
            //        command.Parameters.AddWithValue("@idConnection", idConnection);
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            if (!reader.Read())
            //            {
            //                return null;
            //            }
            //            return GetDbConnectionEntity(reader);
            //        }
            //    }
            //}
        }
        
        // GET CONNECTIONS BY DEPARTURE CITY ID AND ARRIVAL CITY ID
          public List<ConnectionEntityDL> GetConnectionsByDepartureCityIdArrivalCityId(int departureCityId, int arrivalCityId)
        {
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    return accessToDB.ConnectionEntityDLTable.Where(x => x.DepartureCityId == departureCityId)
                                                             .Where(x => x.ArrivalCityId == arrivalCityId)
                                                             .ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex.Message.ToString());
                List<ConnectionEntityDL> nullObject = new List<ConnectionEntityDL>();
                nullObject.Add(new ConnectionEntityDL()); string errorMessage = $"{ex.Message.ToString()}";
                return nullObject;
            }
            
        }
        //public IEnumerable<ConnectionEntityDL> GetConnectionsByDepartureCityIdArrivalCityId(int departureCityId, int arrivalCityId)
        //{
        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT id,departureCityId,arrivalCityId,departureTime,arrivalTime FROM Departures2 WHERE departureCityId = @departureCityId AND arrivalCityId = @arrivalCityId", connection))
        //        {
        //            command.Parameters.AddWithValue("@departureCityId", departureCityId);
        //            command.Parameters.AddWithValue("@arrivalCityId", arrivalCityId);
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    yield return GetDbConnectionEntity(reader);
        //                }
        //            }
        //        }
        //    }
        //}

        // INSERT ONE CONNECTION
        public string InsertConnection(ConnectionEntityDL connectionToDB)
        {
            string result = "";
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    accessToDB.ConnectionEntityDLTable.Add(connectionToDB);
                    accessToDB.SaveChanges();
                    return result = "Die Verbindung wurde gegründet";
                }
            }
            catch (Exception ex)
            {
                return result = $"Die Verbindung wurde nicht gegründet! Fehlerbeschreibung:\n {ex.Message.ToString()}";
            }
            
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("INSERT INTO Departures2(departureCityId,arrivalCityId,departureTime,arrivalTime) VALUES (@departureCity,@arrivalCity,@departureTime,@arrivalTime)", connection))
            //    {
            //        command.Parameters.AddWithValue("@departureCity", connectionToDB.DepartureCityId);
            //        command.Parameters.AddWithValue("@arrivalCity", connectionToDB.ArrivalCityId);
            //        command.Parameters.AddWithValue("@departureTime", connectionToDB.DepartureTime);
            //        command.Parameters.AddWithValue("@arrivalTime", connectionToDB.ArrivalTime);
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        // UPDATE ONE CONNECTION
        public string UpdateConnection(ConnectionEntityDL connectionToDB)
        {
            string result = "";
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    accessToDB.Entry(connectionToDB).State = EntityState.Modified;
                    accessToDB.SaveChanges();
                    return result = "Die Verkehresverbindung wurde korrigiert.";
                }
            }
            catch (Exception ex)
            {
                return result = $"Die Verkehresverbindung wurde nicht korrigiert. Fehlerbeschreibung: {ex.Message.ToString()}";
            }
            
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("UPDATE Departures2 SET departureCityId=@departureCityId, arrivalCityId=@arrivalCityId, departureTime=@departureTime, arrivalTime=@arrivalTime WHERE id=@IdConnection", connection))
            //    {
            //        command.Parameters.AddWithValue("@departureCityId", connectionToDB.DepartureCityId);
            //        command.Parameters.AddWithValue("@arrivalCityId", connectionToDB.ArrivalCityId);
            //        command.Parameters.AddWithValue("@departureTime", connectionToDB.DepartureTime);
            //        command.Parameters.AddWithValue("@arrivalTime", connectionToDB.ArrivalTime);
            //        command.Parameters.AddWithValue("@IdConnection", connectionToDB.Id);
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        // DELETE ONE CONNECTION
        public string DeleteConnection(int idConnection)
        {
            string result = "";
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    accessToDB.Entry(new ConnectionEntityDL(idConnection)).State = EntityState.Deleted;
                    accessToDB.SaveChanges();
                    return result = $"Die Verkehrsverbindung {idConnection} wurde gelöscht";
                }
            }
            catch (Exception ex)
            {
                return result = $"Die Verkehrsverbindung {idConnection} wurde nicht gelöscht. Fehlerbeschreibung: {ex.Message.ToString()}";
            }
            
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("DELETE FROM Departures2 WHERE id=@idConnection", connection))
            //    {
            //        command.Parameters.AddWithValue("@idConnection", idConnection);
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        // ADD ONE MESSAGE FROM WEB SITE
        public string AddMessageFromWebSite(FeedbackEntityDL messageToDB)
        {
            string result = "";
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    accessToDB.FeedbackEntityDLTable.Add(messageToDB);
                    accessToDB.SaveChanges();
                    return result = "Wir danken für Ihre Meinung";
                }
            }
            catch (Exception ex)
            {
                return result = $"Die Bericht wurde nicht gespeichert. Fehlerbeschreibung: {ex.Message.ToString()}";
            }
            
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("INSERT INTO Messages (email, phoneNumber, message, requestType) VALUES (@email, @phoneNumber, @message, @requestType)", connection))
            //    {
            //        command.Parameters.AddWithValue("@email", messageToDB.Email);
            //        command.Parameters.AddWithValue("@phoneNumber", messageToDB.PhoneNumber ?? string.Empty);
            //        command.Parameters.AddWithValue("@message", messageToDB.Message ?? string.Empty);
            //        command.Parameters.AddWithValue("@requestType", messageToDB.RequestType);
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        //private FeedbackEntityDL GetFeedbackEntity(SqlDataReader reader)
        //{
        //    return new FeedbackEntityDL(reader.GetInt32(reader.GetOrdinal("id")),
        //                                reader.GetString(reader.GetOrdinal("requestType")),
        //                                reader.GetString(reader.GetOrdinal("message")) ?? string.Empty,
        //                                reader.GetString(reader.GetOrdinal("email")),
        //                                reader.GetString(reader.GetOrdinal("phoneNumber")) ?? string.Empty
        //                               );
        //}
        // GET ALL MESSAGES
        public List<FeedbackEntityDL> GetMessages()
        {
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    return accessToDB.FeedbackEntityDLTable.ToList();
                }
            }
            catch (Exception ex)
            {
                List<FeedbackEntityDL> error = new List<FeedbackEntityDL>();
                error.Add(new FeedbackEntityDL("Keine Bericht", "wurde eingeselen", "Fehlerbeschreibung:", $"{ex.Message.ToString()}"));
                return error;
            }
            

         //public IEnumerable<FeedbackEntityDL> GetMessages()
         // {
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("SELECT id,requestType,message,email,phoneNumber FROM Messages", connection))
            //    {
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                yield return GetFeedbackEntity(reader);
            //            }
            //        }
            //    }
            //}
          //}
         }

    //GET ONE MESSAGE
    public FeedbackEntityDL GetMessage(int idMessage)
        {
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    return accessToDB.FeedbackEntityDLTable.Where(x => x.Id == idMessage).SingleOrDefault();
                }
            }
            catch(Exception ex)
            {
                FeedbackEntityDL error = new FeedbackEntityDL("Keine Bericht", "wurde eingelesen", "Fehlerbeschreibung:",$"{ex.Message.ToString()}");
                return error;
            }
            
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("SELECT id,requestType,message,email,phoneNumber FROM Messages WHERE id=@idMessage", connection))
            //    {
            //        command.Parameters.AddWithValue("@idMessage", idMessage);
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            if(!reader.Read())
            //            {
            //                return null;
            //            }
            //            return GetFeedbackEntity(reader);
            //        }
            //    }
            //}
        }

        //DELETE ONE MESSAGE
        public string DeleteMessage(int idMessage)
        {
            string result = "";
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    accessToDB.Entry(new FeedbackEntityDL(idMessage)).State = EntityState.Deleted;
                    accessToDB.SaveChanges();
                    return result = $"Der Bericht {idMessage} wurde gelöscht";
                }
            }
            catch (Exception ex)
            {
                return result = $"Der Bericht {idMessage} wurde nicht gelöscht. Fehlerbeschreibung: {ex.Message.ToString()}";
            }
            
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("DELETE FROM Messages WHERE id=@idMessage", connection))
            //    {
            //        command.Parameters.AddWithValue("@idMessage", idMessage);
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        // GET CITIES FROM DB TO CITYCACHE
          public Dictionary<int,CityEntityDL> GetCities()
        {
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    return accessToDB.CityEntityDLTable.ToDictionary(c => c.Id);
                }
            }
            catch (Exception ex)
            {
                Dictionary<int, CityEntityDL> error = new Dictionary<int, CityEntityDL>();
                error.Add(0, new CityEntityDL { CityName = $"Die Angaben wurden nicht durch eingelesen: Fehlerbeschreibung:{ex.Message.ToString()} " });
                return error;
            }
            
        }
        //public IEnumerable<CityEntityDL> GetCities()
        //{
        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT id,city FROM Cities", connection))
        //        {
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var city = new CityEntityDL();
        //                    city.Id = reader.GetInt32(reader.GetOrdinal("id"));
        //                    city.CityName = reader.GetString(reader.GetOrdinal("city"));
        //                    yield return city;
        //                }
        //            }
        //        }
        //    }
        //}

        // INSERT ONE CITY TO DB       
        public string AddCity(string cityName)
        {
            string result = "";
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    accessToDB.CityEntityDLTable.Add(new CityEntityDL { CityName = cityName });
                    accessToDB.SaveChanges();
                    return result = "Anforderung wurde durchgeführt";
                }
            }
            catch(Exception ex)
            {
                return result = $"Die Angaben wurden nicht eingespeichert! Fehlerbeschreibung:\n {ex.Message.ToString()} ";
            }
            
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("INSERT INTO Cities (city) VALUES (@city)", connection))
            //    {
            //        command.Parameters.AddWithValue("@city", cityName);
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        // DELETE ONE CITY IN DB
        public string DeleteCity(int idCity)
        {
            string result = "";
            try
            {
                using (EntityFramework accessToDB = new EntityFramework())
                {
                    accessToDB.Entry(new CityEntityDL { Id = idCity }).State = EntityState.Deleted;
                    accessToDB.SaveChanges();
                    return result = "Anforderung wurde durchgeführt";
                }
            }
            catch (Exception ex)
            {
                return result = $"Die Angaben wurden nicht gelöscht! Fehlerbeschreibung:\n {ex.Message.ToString()}";
            }
            
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("DELETE FROM Cities WHERE id=@idCity", connection))
            //    {
            //        command.Parameters.AddWithValue("@idCity", idCity);
            //        command.ExecuteNonQuery();
            //    }
            //}
        }
    }
}
