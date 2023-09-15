namespace POKA.POC.DDD.Domain.Enums
{
    public enum AppErrorEnum
    {
        [Description("Unknow")]
        Unknow = 0000000,

        #region 400

        [Description("Bad request")]
        BadRequest = 4000000,

        [Description("Invalid parsing of object id.")]
        InvalidParseObjectId = 4000001,

        [Description("Argument null passed.")]
        ArgumentNullPassed = 4000002,

        [Description("Enum value not found.")]
        EnumValueNotFound = 4000003,

        [Description("Argument negative passed.")]
        ArgumentNegativePassed = 4000004,

        [Description("Email already taken.")]
        ConflictEmailTaken = 4000005,

        [Description("Student enrolled to course yet")]
        StudentEnrolledToCourseYet = 4000006,

        [Description("Student age invalid")]
        StudentAgeInvalid = 4000007,

        #endregion

        #region 401

        [Description("Unauthorized")]
        Unauthorized = 4010000,

        #endregion

        #region 403

        [Description("Forbidden")]
        Forbidden = 4030000,

        #endregion

        #region 404

        [Description("Not found")]
        NotFound = 4040000,

        [Description("Student not found.")]
        StudentNotFound = 4040001,

        [Description("Course not found.")]
        CourseNotFound = 4040002,

        [Description("Type not found")]
        TypeNotFound = 4040003,
        
        #endregion

        #region 409

        [Description("Conflict.")]
        Conflict = 4090000,

        #endregion

        #region 500

        [Description("Internal server error")]
        InternalServerError = 5000000,

        #endregion

        #region 501

        [Description("Not implemented")]
        NotImplemented = 5010000

        #endregion
    }
}
