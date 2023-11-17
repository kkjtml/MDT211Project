using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Mime;

public abstract class Post {
    public string Content {get;set;}
    public User Author {get;set;}
    public virtual void Display() {
        Console.WriteLine($"@{Author.Username} | Follower {Author.NumOfFollower} | Following {Author.NumOfFollowing}");
        Console.WriteLine("Added New Post");
        Console.WriteLine($"{Content}");
    }
}
public class TextPost : Post {
    public override void Display() {
        base.Display();
    }
}
public class ImagePost : Post {
    public string ImageURL {get;set;}

    public override void Display() {
        base.Display();
        Console.WriteLine($"Image URL: {ImageURL}.jpg");
    }
}
public class VideoPost : Post {
    public string VideoURL {get;set;}

    public override void Display() {
        base.Display();
        Console.WriteLine($"Video URL: {VideoURL}.mp4");
    }
}

public class User {
    public string Username {get;set;}
    public int NumOfFollower {get;set;}
    public int NumOfFollowing {get;set;}
    private List<User> FollowList = new List<User>();
    private List<Post> PostList = new List<Post>();
    
    public List<User> GetFollowList() {
        List<User> resultList = new List<User>();
        foreach(User user in FollowList) {
            resultList.Add(user);
        }
        return resultList; 
    }
    public void FollowUser(User UserToFollow) {
        if(FollowList.Contains(UserToFollow)) {
            Console.WriteLine($"{this.Username} has already follow {UserToFollow.Username}");
            return;
        }
        FollowList.Add(UserToFollow);
        this.NumOfFollowing++;
        Console.WriteLine($"@{this.Username} follow @{UserToFollow.Username}");
        Console.WriteLine($"@{this.Username} | follower {this.NumOfFollower} | following {this.NumOfFollowing}");
    }
    public List<Post> FetchPost() {
        List<Post> resultList = new List<Post>();
        foreach (Post post in PostList)
        {
            if (post is TextPost || post is ImagePost || post is VideoPost)
            {
                resultList.Add(post);
            }
        }
        return resultList;
    } 
    public void AddTextPost(string content) {
        TextPost newPost = new TextPost
        {
            Author = this,
            Content = content
        };
        PostList.Add(newPost);
    }
       public void AddImagePost(string content, string imageURL) {
        ImagePost newPost = new ImagePost
        {
            Author = this,
            Content = content,
            ImageURL = imageURL
        };
        PostList.Add(newPost);
    }
    public void AddVideoPost(string content, string videoURL) {
        VideoPost newPost = new VideoPost
        {
            Author = this,
            Content = content,
            VideoURL = videoURL
        };
        PostList.Add(newPost);
    }
}

public class Feed {
    public void ShowUserFeed(List<User> users , User user) {
    Console.WriteLine($"---- Show {user.Username} Feed ---");

    foreach (var account in users) {
        foreach (var post in account.FetchPost()) {
            post.Display();
            Console.WriteLine(); 
        }
    }
}
    public void DisplayPosts(List<Post> posts, User user) {
        Console.WriteLine($"---- Show {user.Username} Feed ---");

        foreach (var post in posts)
        {
            post.Display();
            Console.WriteLine();
        }
    }
    public void AddPostFromUserInput(User user) { // User Input ได้
        Console.WriteLine($"---- Show {user.Username} Profile ---");
        Console.WriteLine($"@{user.Username} | Follower {user.NumOfFollower} | Following {user.NumOfFollowing}");
        Console.WriteLine($"Hello, {user.Username}! What's on your mind?");
        Console.WriteLine("1. Text Post");
        Console.WriteLine("2. Image Post with Text");
        Console.WriteLine("3. Video Post with Text");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();

        switch (choice) {
            case "1":
                Console.Write("Enter your text: ");
                string textContent = Console.ReadLine();
                user.AddTextPost(textContent);
                DisplayPosts(user.FetchPost(), user);
                break;

            case "2":
                Console.Write("Enter your text: ");
                string imageContent = Console.ReadLine();
                Console.Write("Enter the image URL: ");
                string imageURL = Console.ReadLine();
                user.AddImagePost(imageContent, imageURL);
                DisplayPosts(user.FetchPost(), user);
                break;

            case "3":
                Console.Write("Enter your text: ");
                string videoContent = Console.ReadLine();
                Console.Write("Enter the video URL: ");
                string videoURL = Console.ReadLine();
                user.AddVideoPost(videoContent, videoURL);
                DisplayPosts(user.FetchPost(), user);
                break;

            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}

public class EventHandlerBase {
    public delegate void Notify();
}
/* public class Button : EventHandlerBase {
    public delegate void Notify();
    public event Notify OnButtonClicked;

    public void SimulateButtonClick() {
        Console.WriteLine("[Button]: Show Shopping Food 🍛🎂");
        Console.WriteLine("[Button]: Button Clicked");
        OnButtonClicked?.Invoke();
    }

}
public class OrderFood {
    public string Food {get;set;}
    public int NumOfFood {get;set;}
    public List<string> DetailsOfFood {get;set;}
    public float PriceOfFood {get;set;}
    private float ShippingCost;
    public float Total {get;set;}

    public OrderFood(string food , int num_of_food , List<string> details_of_food , float price_of_food , float total) {
        this.Food = food;
        this.NumOfFood = num_of_food;
        this.DetailsOfFood = details_of_food;
        this.PriceOfFood = price_of_food;
        this.Total = total;
    }   
}

public class PaymentFood {

}
*/
public class Program {
    static void Main(string[] args) {
        /* Show feed and input post (1)
        User user1 = new User { Username = "AntonLee" , NumOfFollower = 155 , NumOfFollowing = 20 };
        Feed feed = new Feed();
        feed.AddPostFromUserInput(user1);
    
        // Show following (1,2)
        User user2 = new User { Username = "HayatoSano" , NumOfFollower = 100 , NumOfFollowing = 50 };
        user1.FollowUser(user2); */

        /* Show user feed and following feed (3)
        User user1 = new User { Username = "AntonLee" , NumOfFollower = 155 , NumOfFollowing = 20 };
        User user2 = new User { Username = "HayatoSano" , NumOfFollower = 100 , NumOfFollowing = 50 };
        User user3 = new User { Username = "Blade" , NumOfFollower = 179 , NumOfFollowing = 32 };
        user3.AddTextPost("Of five people, three must pay a price...");
        user1.AddImagePost("Review Strawberry Cheesecake🍓🧀🍰 100/10 It tastes great!😍❤ click it!", "StrawberryCheesecake");
        user2.AddVideoPost("with friend #HayatoSano #AntonLee" , "Dance");
        Feed feed = new Feed();
        feed.ShowUserFeed(new List<User> { user3 , user1 , user2 } , user1); */
    }
}