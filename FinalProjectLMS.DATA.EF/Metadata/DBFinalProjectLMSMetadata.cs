using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectLMS.DATA.EF/*.Metadata*/
{
    #region CourseCompletions
    public class CourseCompletionsMetadata
    {
        [Required]
        [Display(Name = "User ID")]
        [StringLength(128, ErrorMessage = "*User ID must not exceed 128 characters.")]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "Course ID")]
        public int CourseID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public System.DateTime DateCompleted { get; set; }
    }

    [MetadataType(typeof(CourseCompletionsMetadata))]
    public partial class CourseCompletions { }
    #endregion

    #region Courses
    public class CoursesMetadata
    {
        [Required]
        [Display(Name = "Course")]
        [StringLength(200, ErrorMessage = "*Course Name must not exceed 200 characters.")]
        public string CourseName { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "*Description must not exceed 500 characters.")]
        public string CourseDescription { get; set; }

        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(CoursesMetadata))]
    public partial class Courses { }
    #endregion

    #region Lessons
    public class LessonsMetadata
    {
        [Required]
        [Display(Name = "Lesson")]
        [StringLength(200, ErrorMessage = "*Lesson title must not exceed 200 characters.")]
        public string LessonTitle { get; set; }

        [Required]
        [Display(Name = "Course ID")]
        public int CourseID { get; set; }

        [StringLength(300, ErrorMessage = "*Introduction must not exceed 300 characters.")]
        public string Introduction { get; set; }

        public string VideoURL { get; set; }

        public string PdfFilename { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }    

    [MetadataType(typeof(LessonsMetadata))]
    public partial class Lessons { }
    #endregion

    #region LessonViews
    public class LessonViewsMetadata
    {
        [Required]
        [Display(Name = "User ID")]
        [StringLength(128, ErrorMessage = "*User ID must not exceed 128 characters.")]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "Lesson ID")]
        public int LessonID { get; set; }

        [Required]
        [Display(Name = "Viewed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public System.DateTime DateViewed { get; set; }
    }

    [MetadataType(typeof(LessonViewsMetadata))]
    public partial class LessonViews { }
    #endregion

    #region UserDetails
    public class UserDetailsMetadata
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "*First name must not exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "*Last name must not exceed 50 characters.")]
        public string LastName { get; set; }
    }

    [MetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetails
    {
        [Display(Name = "Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion
}
