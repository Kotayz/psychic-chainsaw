using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;

public class dbAccess : MonoBehaviour {
	private string connection;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private StringBuilder builder;

	// Use this for initialization
	void Start () {
		
	}
	
	public void OpenDB(string p)
	{
		Debug.Log("Call to OpenDB:" + p);
		// check if file exists in Application.persistentDataPath
		string filepath = Application.persistentDataPath + "/" + p;
		if(!File.Exists(filepath))
		{
			Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
			                 Application.dataPath + "!/assets/" + p);
			// if it doesn't ->
			// open StreamingAssets directory and load the db -> 
			WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
			while(!loadDB.isDone) {}
			// then save to Application.persistentDataPath
			File.WriteAllBytes(filepath, loadDB.bytes);
		}
		
		//open db connection
		connection = "URI=file:" + filepath;
		Debug.Log("Stablishing connection to: " + connection);
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
        CreateTable();
    }
	
	public void CloseDB(){
		reader.Close(); // clean everything up
  	 	reader = null;
   		dbcmd.Dispose();
   		dbcmd = null;
   		dbcon.Close();
   		dbcon = null;
	}

    private void CreateTable()
    {
        string query;
        query = String.Format("CREATE TABLE IF NOT EXISTS THEMESCORES (NAME VARCHAR(30), SCORE INT, CORRECTANSWERS INT, ISACTIVATED VARCHAR(5))");
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            dbcmd.ExecuteNonQuery(); // execute command which returns a reader
        }
        catch (Exception e)
        {

            Debug.Log(e);        
        }        
        PlayerPrefs.SetInt("StatusTable", 1);
        InsertValues();
        Debug.Log("Table Created");

    }

    private void InsertValues()
    {
        string query;
        query = String.Format("INSERT INTO THEMESCORES (NAME, SCORE, CORRECTANSWERS, ISACTIVATED) VALUES ('{0}', '{1}', '{2}', '{3}')", "THEME_1_OPT_1", 0, 0, 1);
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
        }
        catch (Exception e)
        {

            Debug.Log(e);
        }

        query = "";
        query = String.Format("INSERT INTO THEMESCORES (NAME, SCORE, CORRECTANSWERS, ISACTIVATED) VALUES ('{0}', '{1}', '{2}', '{3}')", "THEME_1_OPT_2", 0, 0, 0);
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
        }
        catch (Exception e)
        {

            Debug.Log(e);
        }        
        PlayerPrefs.SetInt("StatusTable", 1);
    }

    public int GetCorrectAnswers(string Name)
    {
        string db = "MasterSQLite.db";
        string filepath = Application.persistentDataPath + "/" + db;
        connection = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + connection);
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        string query;
        query = String.Format("SELECT CORRECTANSWERS FROM THEMESCORES WHERE NAME = '{0}'", Name);
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader

            while (reader.Read())
            {
                int note = reader.GetInt32(0);
                dbcon.Close();
                return note;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        dbcon.Close();
        return 0;
    }

    public int GetFinalNote(string Name)
    {
        string db = "MasterSQLite.db";
        string filepath = Application.persistentDataPath + "/" + db;
        connection = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + connection);
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        string query;
        query = String.Format("SELECT SCORE FROM THEMESCORES WHERE NAME = '{0}'", Name);
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader

            while (reader.Read())
            {
                int note = reader.GetInt32(0);
                dbcon.Close();
                return note;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        dbcon.Close();
        return 0;
    }

    public bool IsActivated(string Name)
    {
        string db = "MasterSQLite.db";
        string filepath = Application.persistentDataPath + "/" + db;
        connection = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + connection);
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        string query;
        query = String.Format("SELECT ISACTIVATED FROM THEMESCORES WHERE NAME = '{0}'", Name);
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader

            while (reader.Read())
            {
                int Activate = reader.GetInt32(0);
                if (Activate == 1)
                {
                    dbcon.Close();
                    return true;
                }
                else
                {
                    dbcon.Close();
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return false;        
    }

    public void UpdateScore(String Name, int Score, float correctAnswers)
    {
        string db = "MasterSQLite.db";
        string filepath = Application.persistentDataPath + "/" + db;
        connection = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + connection);
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        string query;
        query = String.Format("UPDATE THEMESCORES SET SCORE = '{0}', CORRECTANSWERS = '{1}', ISACTIVATED = '{2}' WHERE NAME = '{3}' ", Score, correctAnswers, 1, Name);
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            dbcmd.ExecuteNonQuery(); // execute command which returns a reader
        }
        catch (Exception e)
        {

            Debug.Log(e);
        }
        dbcon.Close();
    }

    public void Activatenext(String Name)
    {
        string db = "MasterSQLite.db";
        string filepath = Application.persistentDataPath + "/" + db;
        connection = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + connection);
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        string query;
        query = String.Format("UPDATE THEMESCORES SET ISACTIVATED = '{0}' WHERE NAME = '{1}'", 1, Name);
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            dbcmd.ExecuteNonQuery(); // execute command which returns a reader
        }
        catch (Exception e)
        {

            Debug.Log(e);
        }
        dbcon.Close();
    }

    //public IDataReader BasicQuery(string query){ // run a basic Sqlite query
    //	dbcmd = dbcon.CreateCommand(); // create empty command
    //	dbcmd.CommandText = query; // fill the command
    //	reader = dbcmd.ExecuteReader(); // execute command which returns a reader
    //	return reader; // return the reader

    //}


    //public bool CreateTable(string name,string[] col, string[] colType){ // Create a table, name, column array, column type array
    //	string query;
    //	query  = "CREATE TABLE " + name + "(" + col[0] + " " + colType[0];
    //	for(var i=1; i< col.Length; i++){
    //		query += ", " + col[i] + " " + colType[i];
    //	}
    //	query += ")";
    //	try{
    //		dbcmd = dbcon.CreateCommand(); // create empty command
    //		dbcmd.CommandText = query; // fill the command
    //		reader = dbcmd.ExecuteReader(); // execute command which returns a reader
    //	}
    //	catch(Exception e){

    //		Debug.Log(e);
    //		return false;
    //	}
    //	return true;
    //}

    public int InsertIntoSingle(string tableName, string colName, string value)
    { // single insert
        string query;
        query = "INSERT INTO " + tableName + "(" + colName + ") " + "VALUES (" + value + ")";
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
        }
        catch (Exception e)
        {

            Debug.Log(e);
            return 0;
        }
        return 1;
    }

    //public int InsertIntoSpecific(string tableName, string[] col, string[] values){ // Specific insert with col and values
    //	string query;
    //	query = "INSERT INTO " + tableName + "(" + col[0];
    //	for(int i=1; i< col.Length; i++){
    //		query += ", " + col[i];
    //	}
    //	query += ") VALUES (" + values[0];
    //	for(int i=1; i< col.Length; i++){
    //		query += ", " + values[i];
    //	}
    //	query += ")";
    //	Debug.Log(query);
    //	try
    //	{
    //		dbcmd = dbcon.CreateCommand();
    //		dbcmd.CommandText = query;
    //		reader = dbcmd.ExecuteReader();
    //	}
    //	catch(Exception e){

    //		Debug.Log(e);
    //		return 0;
    //	}
    //	return 1;
    //}

    //public int InsertInto(string tableName , string[] values ){ // basic Insert with just values
    //	string query;
    //	query = "INSERT INTO " + tableName + " VALUES (" + values[0];
    //	for(int i=1; i< values.Length; i++){
    //		query += ", " + values[i];
    //	}
    //	query += ")";
    //	try
    //	{
    //		dbcmd = dbcon.CreateCommand();
    //		dbcmd.CommandText = query;
    //		reader = dbcmd.ExecuteReader();
    //	}
    //	catch(Exception e){

    //		Debug.Log(e);
    //		return 0;
    //	}
    //	return 1;
    //}

    //public ArrayList SingleSelectWhere(string tableName , string itemToSelect,string wCol,string wPar, string wValue){ // Selects a single Item
    //	string query;
    //	query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;	
    //	dbcmd = dbcon.CreateCommand();
    //	dbcmd.CommandText = query;
    //	reader = dbcmd.ExecuteReader();
    //	//string[,] readArray = new string[reader, reader.FieldCount];
    //	string[] row = new string[reader.FieldCount];
    //	ArrayList readArray = new ArrayList();
    //	while(reader.Read()){
    //		int j=0;
    //		while(j < reader.FieldCount)
    //		{
    //			row[j] = reader.GetString(j);
    //			j++;
    //		}
    //		readArray.Add(row);
    //	}
    //	return readArray; // return matches
    //}

    // Update is called once per frame
    void Update () {
	
	}
}