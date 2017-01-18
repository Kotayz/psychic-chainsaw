//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Data;
//using Mono.Data.SqliteClient;

//public class SQL_Methods : MonoBehaviour
//{

//    private const string urlDataBase = "URI=file:MasterSQLite.db";

//    private IDbConnection _connection;
//    private IDbCommand _command;

//    public void SaveScore(String Name, int Score)
//    {
//        _connection = new SqliteConnection(urlDataBase);
//        _command = _connection.CreateCommand();
//        _connection.Open();

//        string sql = String.Format("INSERT INTO THEME_SCORES (NAME, SCORE, ISACTIVATED) VALUES ('{0}', '{1}', '{2}')", Name, Score, 1);
//        _command.CommandText = sql;
//        _command.ExecuteNonQuery();

//        _connection.Close();
//    }

//    public void UpdateScore(String Name, int Score, float correctAnswers)
//    {
//        _connection = new SqliteConnection(urlDataBase);
//        _command = _connection.CreateCommand();
//        _connection.Open();

//        string sql = String.Format("UPDATE THEMESCORES SET SCORE = '{0}', CORRECTANSWERS = '{1}', ISACTIVATED = '{2}' WHERE NAME = '{3}' ", Score, correctAnswers, 1, Name);
//        _command.CommandText = sql;
//        _command.ExecuteNonQuery();

//        _connection.Close();
//    }

//    public void Activatenext(String Name)
//    {
//        _connection = new SqliteConnection(urlDataBase);
//        _command = _connection.CreateCommand();
//        _connection.Open();

//        string sql = String.Format("UPDATE THEMESCORES SET ISACTIVATED = '{0}' WHERE NAME = '{1}'", 1, Name);
//        _command.CommandText = sql;
//        _command.ExecuteNonQuery();

//        _connection.Close();
//    }

//    public bool IsActivated(string Name)
//    {
//        _connection = new SqliteConnection(urlDataBase);
//        _command = _connection.CreateCommand();
//        _connection.Open();

//        string sqlQuery = String.Format("SELECT ISACTIVATED FROM THEMESCORES WHERE NAME = '{0}'", Name);
//        _command.CommandText = sqlQuery;
//        IDataReader reader = _command.ExecuteReader();

//        while (reader.Read())
//        {
//            int Activate = reader.GetInt32(0);
//            if (Activate == 1)
//            {
//                _connection.Close();
//                return true;
//            }
//            else
//            {
//                _connection.Close();
//                return false;
//            }
//        }
//        _connection.Close();
//        return false;
//    }

//    public int GetCorrectAnswers(string Name)
//    {
//        _connection = new SqliteConnection(urlDataBase);
//        _command = _connection.CreateCommand();
//        _connection.Open();

//        string sqlQuery = String.Format("SELECT CORRECTANSWERS FROM THEMESCORES WHERE NAME = '{0}'", Name);
//        _command.CommandText = sqlQuery;
//        IDataReader reader = _command.ExecuteReader();

//        while (reader.Read())
//        {
//            int note = reader.GetInt32(0);
//            _connection.Close();
//            return note;
//        }

//        _connection.Close();
//        return 0;
//    }

//    public int GetFinalNote(string Name)
//    {
//        _connection = new SqliteConnection(urlDataBase);
//        _command = _connection.CreateCommand();
//        _connection.Open();

//        string sqlQuery = String.Format("SELECT SCORE FROM THEMESCORES WHERE NAME = '{0}'", Name);
//        _command.CommandText = sqlQuery;
//        IDataReader reader = _command.ExecuteReader();

//        while (reader.Read())
//        {
//            int note = reader.GetInt32(0);
//            _connection.Close();
//            return note;
//        }

//        _connection.Close();
//        return 0;
//    }

//    public void Select()
//    {
//        string sqlQuery = String.Format("SELECT * FROM HIGHSCORES");

//        _command.CommandText = sqlQuery;

//        IDataReader reader = _command.ExecuteReader();

//        while (reader.Read())
//        {
//            string name = reader.GetString(0);
//            int score = reader.GetInt32(1);

//            Debug.Log("name =" + name + " score =" + score);
//        }
//    }
//}
