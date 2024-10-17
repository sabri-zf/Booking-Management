using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ClsReviews
    {
        enum Mode { Add ,Edit}
        Mode? _mode = null;

        public int? ID { get; private set;}
        public int? UserID { get;set;}
        public bool ReviewStatus {get; set;}
        public string? Comment { get; set;}



        public ClsReviews()
        {
            this.ID = null;
            this.UserID = null;
            this.Comment = null;
            this.ReviewStatus = false;
            this._mode = Mode.Add;
        }

        private ClsReviews(int iD, int userID, bool reviewStatus, string? comment)
        {
            this._mode = Mode.Edit;
            this.ID = iD;
            this.UserID = userID;
            this.ReviewStatus = reviewStatus;
            this.Comment = comment;
        }

        public static ClsReviews? FindByID(int iD)
        {
            return null;
        }

        public static ClsReviews? FindByUserID(int iD)
        {
            return null;
        }


        public static bool DeleteReview(int iD)
        {
            return false;
        }

        private bool _AddNew()
        {
            return true;
        }

        private bool _Update()
        {
            return true;
        }

        public bool Save()
        {
            return true;
        }

        public static DataTable? GetEachReviews()
        {
            return null;
        }

    }
}
