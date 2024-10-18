using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ClsReviews
    {
        enum Mode { Add, Edit }
        Mode? _mode = null;

        public int? ID { get; private set; }
        public int? UserID { get; set; }
        public bool ReviewStatus { get; set; }
        public string Comment { get; set; }



        public ClsReviews()
        {
            this.ID = null;
            this.UserID = null;
            this.Comment = null;
            this.ReviewStatus = false;
            this._mode = Mode.Add;
        }

        private ClsReviews(int iD, int userID, bool reviewStatus, string comment)
        {
            this._mode = Mode.Edit;
            this.ID = iD;
            this.UserID = userID;
            this.ReviewStatus = reviewStatus;
            this.Comment = comment;
        }

        public static ClsReviews FindByID(int iD)
        {
            int UserID = -1;
            bool Status = false;
            string Comment = string.Empty;

            if(Reviews.FindByID(iD ,ref UserID,ref Status,ref Comment))
            {
                return new ClsReviews(iD,UserID,Status,Comment);
            }

            return null;
        }

        public static ClsReviews FindByUserID(int UserID)
        {
            int ID = -1;
            bool Status = false;
            string Comment = string.Empty;

            if (Reviews.FindByUserID(ref ID,  UserID, ref Status, ref Comment))
            {
                return new ClsReviews(ID, UserID, Status, Comment);
            }

            return null;

        }


        public static bool DeleteReviewByID(int ID)
        {
            return Reviews.DeleteByID(ID);
        }
        public static bool DeleteReviewByUserID(int UserID)
        {
            return Reviews.DeleteByID(UserID);
        }

        private bool _AddNew()
        {
            this.ID = Reviews.AddNewReview(this.UserID,this.ReviewStatus,this.Comment);

            return (this.ID != null || this.ID > 0);
        }

        private bool _Update()
        {

            return Reviews.UpdateReview(this.ID,this.UserID,this.ReviewStatus,this.Comment);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case Mode.Add:
                    if (_AddNew())
                    {
                        _mode = Mode.Edit;
                        return true;
                    }
                    return false;
                    case Mode.Edit:
                    return _Update();
            }
            return false;
        }

        public static async Task<DataTable> GetEachReviewsAsync()
        {
            return await Reviews.GetEeachReviewsAsync();
        }

        public static DataTable GetEachReviewsSync()
        {
            return  Reviews.GetEeachReviewsSync();
        }

    }
}
