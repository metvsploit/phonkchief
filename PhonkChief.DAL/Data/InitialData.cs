using PhonkChief.Domain.Entities;
using PhonkChief.Domain.Enum;
using PhonkChief.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace PhonkChief.DAL.Data
{
    public static class InitialData
    {
        private static string path = Directory.GetCurrentDirectory();

        private static readonly FileToBytes _file = new FileToBytes();
        private static readonly User Admin = new User
        {
           
            Email = "admin@mail.ru",
            Password = "123456".Hash(),
            Country = "Russia",
            City = "Sterlitamak",
            NickName = "admin",
            Role = Role.Admin,
            Avatar = _file.ConvertToBytes($"{path}/InitialFiles/default.png"),
            DateOfReg = DateTime.Now,
        };

        public static List<User> Users = new List<User>
        {
            new User { Email="user@mail.ru", Password="123456".Hash(),
                Country = "Russia", City="Moscow", NickName="nagibator",
                Role = Role.User, Avatar = _file.ConvertToBytes($"{path}/InitialFiles/default.png"),
                DateOfReg = DateTime.Now, },

             Admin
        };

        public static List<Loop> Loops = new List<Loop>
        {
            new Loop {Bpm = 130, Category = Category.Memphis,
                Date = DateTime.Now, Name = "90s Memphis Drum",
                File = _file.ConvertToBytes($"{path}/InitialFiles/loop/phonkchief_90s_memphis_drum_3.wav"),

                User = Admin},

            new Loop { Bpm = 115, Category = Category.Horror,
                Date = DateTime.Now, Name = "Phonk shit",
                File = _file.ConvertToBytes($"{path}/InitialFiles/loop/phonkchief_horror_phonk_2.wav"),
             
                User = Admin},

            new Loop { Bpm = 161, Category= Category.Phonk,
                Date = DateTime.Now, Name = "Horror phonk",
                File = _file.ConvertToBytes($"{path}/InitialFiles/loop/phonkchief_drift_phonk_cowbells_1.wav"),
               
                User = Admin},
        };
    }
}
