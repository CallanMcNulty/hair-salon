using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private string _availability;
    private string _notes;
    public Stylist(string Name, string Availability, string Notes, int Id=0)
    {
      _id = Id;
      _name = Name;
      _availability = Availability;
      _notes = Notes;
    }
    public int GetId()
    {
      return _id;
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
        return (_id==newStylist.GetId());
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
