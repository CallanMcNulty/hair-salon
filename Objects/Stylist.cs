using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace HairSalon
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private string _availability;
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
    public string availability
    {
      get
      {
        return _availability;
      }
      set
      {
        _availability = value;
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
    public Stylist(string Name, string Availability, string Notes, int Id=0)
    {
      _id = Id;
      _name = Name;
      _availability = Availability;
      _notes = Notes;
    }
    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        return (_id==newStylist.id);
      }
    }
    public void Save()
    {
      DBObjects dbo = DBObjects.CreateCommand("INSERT INTO stylists (name, availability, notes) OUTPUT INSERTED.id VALUES (@Name, @Avail, @Notes);", new List<string> {"@Name", "@Avail", "@Notes"},  new List<object> {_name, _availability, _notes});
      SqlDataReader rdr = dbo.RDR;
      rdr = dbo.CMD.ExecuteReader();

      while(rdr.Read())
      {
        _id = rdr.GetInt32(0);
      }

      dbo.Close();
    }
    public static Stylist Find(int findId)
    {
      DBObjects dbo = DBObjects.CreateCommand("SELECT * FROM stylists WHERE id=@Id;", new List<string> {"@Id"},  new List<object> {findId});
      SqlDataReader rdr = dbo.RDR;
      rdr = dbo.CMD.ExecuteReader();

      Stylist result = null;
      while(rdr.Read())
      {
        result = new Stylist(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(0));
      }

      dbo.Close();
      return result;
    }
    public void Update(List<string> updateColumns, List<object> updateValues)
    {
      for(int i=0; i<updateColumns.Count; i++)
      {
        string currentColumn = updateColumns[i];
        DBObjects dbo = DBObjects.CreateCommand("UPDATE stylists SET "+currentColumn+" = @NewValue WHERE id=@Id;", new List<string> {"@NewValue", "@Id"}, new List<object> {updateValues[i], _id});
        dbo.CMD.ExecuteNonQuery();
        PropertyInfo propertyInfo = this.GetType().GetProperty(currentColumn);
        propertyInfo.SetValue(this, Convert.ChangeType(updateValues[i], propertyInfo.PropertyType), null);
      }
    }
    public static List<Stylist> GetAll()
    {
      DBObjects dbo = DBObjects.CreateCommand("SELECT * FROM stylists;");
      SqlDataReader rdr = dbo.RDR;
      rdr = dbo.CMD.ExecuteReader();

      List<Stylist> allStylists = new List<Stylist> {};
      while(rdr.Read())
      {
        allStylists.Add( new Stylist(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(0)) );
      }

      dbo.Close();
      return allStylists;
    }
    public static void DeleteAll()
    {
      DBObjects dbo = DBObjects.CreateCommand("DELETE FROM stylists;");
      dbo.CMD.ExecuteNonQuery();
      dbo.Close();
    }

  }
}
