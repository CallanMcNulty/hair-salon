using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace HairSalon
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylistId;
    private string _notes;
    public int id
    {
      get
      {
        return _id;
      }
      set
      {
        _id = value;
      }
    }
    public string name
    {
      get
      {
        return _name;
      }
      set
      {
        _name = value;
      }
    }
    public int stylist_id
    {
      get
      {
        return _stylistId;
      }
      set
      {
        _stylistId = value;
      }
    }
    public string notes
    {
      get
      {
        return _notes;
      }
      set
      {
        _notes = value;
      }
    }
    public Client(string Name, int StylistId, string Notes, int Id=0)
    {
      _id = Id;
      _name = Name;
      _stylistId = StylistId;
      _notes = Notes;
    }
    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        return (_id==newClient.id);
      }
    }
    public void Save()
    {
      DBObjects dbo = DBObjects.CreateCommand("INSERT INTO clients (name, stylist_id, notes) OUTPUT INSERTED.id VALUES (@Name, @StylistId, @Notes);", new List<string> {"@Name", "@StylistId", "@Notes"},  new List<object> {_name, _stylistId, _notes});
      SqlDataReader rdr = dbo.RDR;
      rdr = dbo.CMD.ExecuteReader();

      while(rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }

      dbo.Close();
    }
    public static Client Find(int findId)
    {
      DBObjects dbo = DBObjects.CreateCommand("SELECT * FROM clients WHERE id=@Id;", new List<string> {"@Id"},  new List<object> {findId});
      SqlDataReader rdr = dbo.RDR;
      rdr = dbo.CMD.ExecuteReader();

      Client result = null;
      while(rdr.Read())
      {
        result = new Client(rdr.GetString(1), rdr.GetInt32(2), rdr.GetString(3), rdr.GetInt32(0));
      }

      dbo.Close();
      return result;
    }
    public void Update(List<string> updateColumns, List<object> updateValues)
    {
      for(int i=0; i<updateColumns.Count; i++)
      {
        string currentColumn = updateColumns[i];
        DBObjects dbo = DBObjects.CreateCommand("UPDATE clients SET "+currentColumn+" = @NewValue WHERE id=@Id;", new List<string> {"@NewValue", "@Id"}, new List<object> {updateValues[i], _id});
        dbo.CMD.ExecuteNonQuery();
        PropertyInfo propertyInfo = this.GetType().GetProperty(currentColumn);
        propertyInfo.SetValue(this, updateValues[i], null);
      }
    }
    public void Delete()
    {
      DBObjects dbo = DBObjects.CreateCommand("DELETE FROM clients WHERE id=@Id;", new List<string> {"@Id"},  new List<object> {_id});
      dbo.CMD.ExecuteNonQuery();
      dbo.Close();
    }
    public static List<Client> GetAll()
    {
      DBObjects dbo = DBObjects.CreateCommand("SELECT * FROM clients;");
      SqlDataReader rdr = dbo.RDR;
      rdr = dbo.CMD.ExecuteReader();

      List<Client> allClients = new List<Client> {};
      while(rdr.Read())
      {
        allClients.Add( new Client(rdr.GetString(1), rdr.GetInt32(2), rdr.GetString(3), rdr.GetInt32(0)) );
      }

      dbo.Close();
      return allClients;
    }
    public static void DeleteAll()
    {
      DBObjects dbo = DBObjects.CreateCommand("DELETE FROM clients;");
      dbo.CMD.ExecuteNonQuery();
      dbo.Close();
    }
  }
}
