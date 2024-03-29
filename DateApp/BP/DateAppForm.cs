﻿using DateApp.Helpers;
using DateApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DateApp
{
    public partial class DateAppForm : Form
    {
        private List<Person> people = new List<Person>();
        private List<PersonSeeking> peopleSeeking = new List<PersonSeeking>();

        private DataAccess db = new DataAccess();
        private GUIHelper gh = new GUIHelper();

        public DateAppForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Happens doing loadtime.
        /// Responsible for getting correct names for combo boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form1_Load(object sender, EventArgs e)
        {
            object[] prof = db.GetValues("DateApp", "SELECT prof FROM profession");
            object[] status = db.GetValues("DateApp", "SELECT status FROM status");

            int px = 0;
            int ps = 0;

            // Old test code. THE LAST LINE WORKS TO GET INFO AND IS CURRENTLY IN USE!
            //object test = obj[0];
            //string test1 = ((Dapper.SqlMapper.DapperRow)obj[0]).values[0];
            //var test11 = s[0][1];
            //var s = obj.AsQueryable();
            //var st = ((object[])((System.Collections.Generic.IDictionary<string, object>)obj[0]).Values)[0];

            foreach (object p in prof)
            {
                profBox.Items.Add(((object[])((IDictionary<string, object>)prof[px]).Values)[0].ToString());
                px++;
            }
            foreach (object s in status)
            {
                statusBox.Items.Add(((object[])((IDictionary<string, object>)status[ps]).Values)[0].ToString());
                ps++;
            }
        }

        #region UserInteractions

        /// <summary>
        /// Click handler for Inser button.
        /// Extracts inputet data and call insert to DB.
        /// </summary>
        /// <param name="sender"> Button. </param>
        /// <param name="e"> Event. </param>
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            string result;
            string[] values =
                {
                firstnameBox.Text,
                lastnameBox.Text,
                mailBox.Text,
                genderBox.Text,
                birthdayBox.Text,
                profBox.Text,
                postNumberBox.Text,
                statusBox.Text,
                seekingBox.Text
            };

            // Make sure there is no null values
            foreach (string v in values)
            {
                if (v == "" || v == null)
                {
                    // Display message and return
                    MessageBox.Show("Hey there! You can not create a user with an empty field(s)", "HEY YOU!");
                    return;
                }
            }

            openFileDialog1.Filter = "JPG files|*.jpg";
            openFileDialog1.Title = "Select a jpg image File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;

                // Call to insert person
                result = db.InsertPeople(values, file);

                // Show picture chosen
                profilePicture.Image = Image.FromFile(file);

                // Show result
                MessageBox.Show(result, "New User");
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Click handler for Query Males button.
        /// Query and display in ListView.
        /// </summary>
        /// <param name="sender"> Button. </param>
        /// <param name="e"> Event. </param>
        private void buttonQueryMales_Click(object sender, EventArgs e)
        {
            // Query database
            peopleSeeking = db.GetPeople("DateApp",
                "SELECT firstName, lastName, seeking, area.city, area.state FROM person " +
                "INNER JOIN area ON person.area = area.areaID " +
                "WHERE gender = 'M'");

            // Call GUIHelper function to populate listview
            gh.ListPeopleView(listViewPerson, peopleSeeking);
        }

        /// <summary>
        /// Click handler for Query Females button.
        /// Query and display in ListView.
        /// </summary>
        /// <param name="sender"> Button. </param>
        /// <param name="e"> Event. </param>
        private void buttonQueryFemales_Click(object sender, EventArgs e)
        {
            // Query database
            peopleSeeking = db.GetPeople("DateApp",
                "SELECT firstName, lastName, seeking, area.city, area.state FROM person " +
                "INNER JOIN area ON person.area = area.areaID " +
                "WHERE gender = 'F'");

            // Call GUIHelper function to populate listview
            gh.ListPeopleView(listViewPerson, peopleSeeking);
        }

        /// <summary>
        /// Click handler for image button.
        /// Displays a chosen JPG file in pictureBox.
        /// </summary>
        /// <param name="sender"> Button. </param>
        /// <param name="e"> Event. </param>
        private void buttonImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG files|*.jpg";
            openFileDialog1.Title = "Select a jpg image File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                profilePicture.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        #endregion

    }
}