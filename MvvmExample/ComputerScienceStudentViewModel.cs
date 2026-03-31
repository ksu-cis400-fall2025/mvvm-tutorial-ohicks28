using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// notifies when a property of this class changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// private student backung field
        /// </summary>
        private Student _student { get; set; }
        /// <summary>
        /// student's first name
        /// </summary>
        public string FirstName => _student.FirstName;

        public IEnumerable<CourseRecord> CourseRecords => _student.CourseRecords;

        /// <summary>
        /// student's last name
        /// </summary>
        public string LastName => _student.LastName;
        /// <summary>
        /// 
        /// </summary>
        public double GPA => _student.GPA;

        /// <summary>The student's Computer Science GPA</summary>
        public double ComputerScienceGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords)
                {
                    if (cr.CourseName.Contains("CIS"))
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }
                }
                return points / hours;
            }
        }
        /// <summary>
        /// event handler for CS student GPA changed
        /// </summary>
        /// <param name="sender">the student that is changing</param>
        /// <param name="e">event args describing the changed GPA</param>
        private void HandleStudentPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Student.GPA))
            {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComputerScienceGPA)));
            }
        }

        /// <summary>
        /// Constructs a new ComputerScienceViewModel
        /// </summary>
        /// <param name="student">student to base ViewModel on</param>
        public ComputerScienceStudentViewModel(Student student)
        {
            _student = student;
            student.PropertyChanged += HandleStudentPropertyChanged;
        }

    }
}
