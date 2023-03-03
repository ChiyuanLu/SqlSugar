﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Test.Model;
namespace OrmTest
{
    public class UnitNavUpdatee12
    {
        public static void Init()
        {
            var db = NewUnitTest.Db;
            db.CodeFirst.InitTables<Student, School, Playground, Room, Book>();
          
            Demo1(db);
            Demo2(db);
        }

        private static void Demo1(SqlSugarClient db)
        {
            db.DbMaintenance.TruncateTable<Student, School, Playground, Room, Book>();
            db.InsertNav(new Student()
            {
                Age = "11",
                Name = "a",
                Books = new List<Book>() { new Book() { Name = "book" } },
                Schools = new List<School>() {
                   new School(){
                       Name="学校",
                       Playgrounds=new List<Playground>()
                       {
                           new Playground(){  Name="学校GR"}

                       } ,
                       Rooms=new List<Room>(){
                             new Room() {  Name= "学校ROOM" } } }

                 }

            })
            .Include(s => s.Books)
            //   .Include(s => s.Schools)
            .Include(s => s.Schools).ThenInclude(sc => sc.Rooms)
            .Include(s => s.Schools).ThenInclude(sc => sc.Playgrounds)
            .ExecuteCommand();
            var data = db.Queryable<Student>()
                .Includes(s => s.Books)
                .Includes(s => s.Schools, s => s.Rooms)
                .Includes(s => s.Schools, s => s.Playgrounds)
                .ToList();
            if (data.Count != 1 || data.First().Schools.Count != 1 || data.First().Schools.First().Rooms.Count() != 1 || data.First().Schools.First().Playgrounds.Count() != 1)
            {
                throw new Exception("unit error");
            }
        }
        private static void Demo2(SqlSugarClient db)
        {
            db.DbMaintenance.TruncateTable<Student, School, Playground, Room, Book>();
            db.InsertNav(new Student()
            {
                Age = "11",
                Name = "a",
                Books = new List<Book>() { new Book() { Name = "book" } },
                Schools = new List<School>() {
                   new School(){
                       Name="学校",
                       Playgrounds=new List<Playground>()
                       {
                           new Playground(){  Name="学校GR"}

                       } ,
                       Rooms=new List<Room>(){
                             new Room() {  Name= "学校ROOM" } } }

                 }

            })

            //   .Include(s => s.Schools)
            .Include(s => s.Schools).ThenInclude(sc => sc.Rooms)
            .Include(s => s.Schools).ThenInclude(sc => sc.Playgrounds)
            .Include(s => s.Books)
            .ExecuteCommand();
            var data = db.Queryable<Student>()
                .Includes(s => s.Books)
                .Includes(s => s.Schools, s => s.Rooms)
                .Includes(s => s.Schools, s => s.Playgrounds)
                .ToList();
            if (data.Count != 1 || data.First().Schools.Count != 1 || data.First().Schools.First().Rooms.Count() != 1 || data.First().Schools.First().Playgrounds.Count() != 1)
            {
                throw new Exception("unit error");
            }
        }
    }
}