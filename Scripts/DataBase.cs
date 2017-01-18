using UnityEngine;
using System.Data;
using System;
using Mono.Data.SqliteClient;
using System.IO;
using System.Collections;

public class DataBase : MonoBehaviour
{
    private const string p = "MasterSQLite.db";
    private string description;
    private string connection;
    private IDbConnection _connection;
    private IDbCommand _command;
    private IDataReader reader;
    private string sql;
    private int status;

    void Start()
    {
        dbAccess db = new dbAccess();
        db.OpenDB("MasterSQLite.db");        
    }

    //void OnGUI()
    //{
    //    // create the gui text of the description
    //    GUI.Box(new Rect(5, 5, Screen.width - 10, Screen.height / 3), description);
    //    if (GUI.skin.customStyles.Length > 0)
    //        GUI.skin.customStyles[0].wordWrap = true;
    //}

    private void CreateTable()
    {
        sql = String.Format("CREATE TABLE IF NOT EXISTS THEMESCORES (NAME VARCHAR(30), SCORE INT, CORRECTANSWERS INT, ISACTIVATED VARCHAR(5))");
        _command.CommandText = sql;
        _command.ExecuteNonQuery();
        PlayerPrefs.SetInt("StatusTable", 1);
        InsertValues();
        Debug.Log("Table Created");

    }

    private void InsertValues()
    {
        sql = String.Format("INSERT INTO THEMESCORES (NAME, SCORE, CORRECTANSWERS, ISACTIVATED) VALUES ('{0}', '{1}', '{2}', '{3}')", "THEME_1_OPT_1", 0, 0, 1);
        _command.CommandText = sql;
        _command.ExecuteNonQuery();

        sql = String.Format("INSERT INTO THEMESCORES (NAME, SCORE, CORRECTANSWERS, ISACTIVATED) VALUES ('{0}', '{1}', '{2}', '{3}')", "THEME_1_OPT_2", 0, 0, 0);
        _command.CommandText = sql;
        _command.ExecuteNonQuery();
        _connection.Close();
        PlayerPrefs.SetInt("StatusTable", 1);
    }
}
