using WineReviewsApplication.Models;

namespace WineReviewsApplication.Interface
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAWine(int wineId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool Save();

        bool DeleteReview(Review review);
        //To delete range of reviews, "delete range"
        bool DeleteReviews(List<Review> reviews);



    }
}
