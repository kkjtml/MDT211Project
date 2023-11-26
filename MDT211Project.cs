using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using System.Xml.Serialization;

public enum Visibility
{
    PRIVATE,
    PUBLIC,
}

public class EventHandlerBase
{
    public delegate void Notify();
}

public class Button : EventHandlerBase
{
    public delegate void Notify();
    public event Notify OnButtonClicked;
    public void SimulateButtonFoodShopping()
    {
        Console.WriteLine("[Button]: Food Shopping 🍽︎ ");
        Console.WriteLine("[Button]: Button Clicked");
        OnButtonClicked?.Invoke();
    }
    public void SimulateWindow()
    {
        Console.WriteLine("[Window]: Order Food");
        OnButtonClicked?.Invoke();
    }
}

public class Food
{
    public string Foodname { get; set; }
    public float FoodPrice { get; set; }

    public Food(string foodName, float foodPrice)
    {
        Foodname = foodName;
        FoodPrice = foodPrice;
    }
}

public class FoodInventory
{
    private Dictionary<string, Food> _food = new Dictionary<string, Food>();
    public int Quantity { get; set; } //จำนวนอาหาร
    private string FoodDetail { get; set; } //เช่น ไม่หวาน
    public float Total;

    public void AddFoodInInventory(string foodName, float foodPrice)
    {
        Console.WriteLine();
        Console.WriteLine($"⊱┄┄┄ Add food to the food inventory┄┄┄⊰");
        Console.WriteLine();
        if (_food.ContainsKey(foodName))
        {
            Console.WriteLine($"Food: {foodName} , Price: {foodPrice} baht has already been added to the food inventory");
            Console.WriteLine();
            Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
            return;
        }
        Food food = new Food(foodName, foodPrice);
        _food.Add(foodName, food);
        Console.WriteLine($"Food: {foodName} , Price: {foodPrice} baht added to the food inventory");
        Console.WriteLine();
        Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
    }

    public void AddFoodInPost(string foodName, float foodPrice)
    {
        if (_food.ContainsKey(foodName))
        {
            Console.WriteLine($"Food: {foodName} , Price: {foodPrice} baht");
            return;
        }
        Food food = new Food(foodName, foodPrice);
        _food.Add(foodName, food);
        Console.WriteLine($"Food: {foodName} , Price: {foodPrice} baht");
    }

    public void SearchFoodName(string foodName)
    {
        Console.WriteLine();
        Console.WriteLine($"⊱┄┄┄ Search for food ┄┄┄⊰");
        Console.WriteLine();
        Console.WriteLine($"Finding food {foodName} ...");
        foreach (var checkfood in _food)
        {
            if (_food.ContainsKey(foodName))
            {
                Food food = _food[foodName];
                Console.WriteLine($"Found food {foodName} , Price: {food.FoodPrice} baht");
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                return;
            }
        }
        Console.WriteLine($"Not found food {foodName}");
        Console.WriteLine();
        Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
    }

    public bool Checkfood(string foodName)
    {
        bool check = true;
        foreach (var checkfood in _food)
        {
            if (_food.ContainsKey(foodName))
            {

                return check;
            }
        }
        return !check;
    }
    public float GetFoodprice(string foodName)
    {
        Food food = _food[foodName];
        return food.FoodPrice;
    }

    public void OrderFood(string foodName, float quantity, string foodDetail)
    {
        Console.WriteLine();
        foreach (var checkfood in _food)
        {
            if (_food.ContainsKey(foodName))
            {
                Food food = _food[foodName];
                Console.WriteLine("┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄");
                Console.WriteLine("⊱┄┄┄ Order summary ┄┄┄⊰");
                Console.WriteLine($"Food: {foodName}");
                Console.WriteLine($"Price: {food.FoodPrice} baht");
                Console.WriteLine($"Quantity: {quantity}");
                Console.WriteLine($"Food detail: {foodDetail}");
                Console.WriteLine($"Shipping cost: 20 baht");
                float ShippingCost = 20;
                Total = (quantity * food.FoodPrice) + ShippingCost;
                Console.WriteLine($"Total: {Total} baht");
                Console.WriteLine("┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄");
                Console.WriteLine();
                return;
            }
        }
        Console.WriteLine("┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄");
        Console.WriteLine($"Food: {foodName} isn't in the food inventory");
        Console.WriteLine("┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄");
        Console.WriteLine();
    }

    public float GetTotal()
    {
        return Total;
    }

    public bool Payment(float total, float pay)
    {
        bool check = true;
        Total = total;
        if (pay == total)
        {
            return check;
        }
        else
        {
            return !check;
        }
    }
}

public abstract class Post
{
    public string Content { get; set; }
    public User Author { get; set; }
    public Visibility PostVisibility { get; set; }

    public abstract void Display();
}

public class TextPost : Post
{
    public override void Display()
    {
        Console.WriteLine($"@{Author.Username} | Follower {Author.NumOfFollower} | Following {Author.NumOfFollowing}");
        Console.WriteLine($"Added Text Post ✎ : ❝{Content}❞ ");
    }
}

public class ImagePostWithText : Post
{
    public string ImageURL { get; set; }

    public override void Display()
    {
        Console.WriteLine($"@{Author.Username} | Follower {Author.NumOfFollower} | Following {Author.NumOfFollowing}");
        Console.WriteLine($"Added Image Post ✎ : ❝{Content}❞ ");
        Console.WriteLine($"Image URL 📷︎ : {ImageURL}.jpg");
    }
}

public class VideoPostWithText : Post
{
    public string VideoURL { get; set; }

    public override void Display()
    {
        Console.WriteLine($"@{Author.Username} | Follower {Author.NumOfFollower} | Following {Author.NumOfFollowing}");
        Console.WriteLine($"Added Video Post ✎ : ❝{Content}❞ ");
        Console.WriteLine($"Video URL 📹︎ : {VideoURL}.mp4");
    }
}

public class ImagePostWithTextAndFoodShopping : Post
{
    public string ImageURL { get; set; }
    public string FoodNameShopping { get; set; }
    public float FoodPriceShopping { get; set; }
    FoodInventory foodInventory = new FoodInventory();
    Button button = new Button();

    public override void Display()
    {
        Console.WriteLine($"@{Author.Username} | Follower {Author.NumOfFollower} | Following {Author.NumOfFollowing}");
        Console.WriteLine($"Added Image Post ✎ : ❝{Content}❞ ");
        Console.WriteLine($"Image URL 📷︎ : {ImageURL}.jpg");
        button.SimulateButtonFoodShopping();
        button.SimulateWindow();
        foodInventory.AddFoodInPost(FoodNameShopping, FoodPriceShopping);
    }
}

public class VideoPostWithTextAndFoodShopping : Post
{
    public string VideoURL { get; set; }
    public string FoodNameShopping { get; set; }
    public float FoodPriceShopping { get; set; }
    FoodInventory foodInventory = new FoodInventory();
    Button button = new Button();

    public override void Display()
    {
        Console.WriteLine($"@{Author.Username} | Follower {Author.NumOfFollower} | Following {Author.NumOfFollowing}");
        Console.WriteLine($"Added Video Post ✎ : ❝{Content}❞ ");
        Console.WriteLine($"Video URL 📹︎ : {VideoURL}.mp4");
        button.SimulateButtonFoodShopping();
        button.SimulateWindow();
        foodInventory.AddFoodInPost(FoodNameShopping, FoodPriceShopping);
    }
}

public class User
{
    public string Username { get; set; }
    public int NumOfFollower { get; set; } //จำนวนผู้ติดตาม
    public int NumOfFollowing { get; set; } //จำนวนกำลังติดตาม

    private List<User> FollowList = new List<User>();
    private List<Post> PostList = new List<Post>();

    public List<User> GetFollowList()
    {
        List<User> resultList = new List<User>();
        foreach (User user in FollowList)
        {
            resultList.Add(user);
        }
        return resultList;
    }

    public void FollowUser(User userToFollow)
    {
        Console.WriteLine();
        Console.WriteLine($"⊱┄┄┄ Follow ┄┄┄⊰");
        Console.WriteLine();
        if (FollowList.Contains(userToFollow))
        {
            Console.WriteLine($"@{this.Username} has already follow @{userToFollow.Username}");
            Console.WriteLine($"@{this.Username} | follower {this.NumOfFollower} | following {this.NumOfFollowing}");
            Console.WriteLine();
            Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
            return;
        }
        FollowList.Add(userToFollow);
        this.NumOfFollowing++;
        userToFollow.NumOfFollower++;
        Console.WriteLine($"@{this.Username} follow @{userToFollow.Username}");
        Console.WriteLine($"@{this.Username} | follower {this.NumOfFollower} | following {this.NumOfFollowing}");
        Console.WriteLine();
        Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
    }

    public List<Post> FetchPublicPost()
    {
        List<Post> resultList = new List<Post>();
        foreach (Post post in PostList)
        {
            if (post.PostVisibility == Visibility.PUBLIC)
            {
                resultList.Add(post);
            }
        }
        return resultList;
    }

    public void AddTextPost(string content, Visibility visibility)
    {
        TextPost newPost = new TextPost
        {
            Author = this,
            PostVisibility = visibility,
            Content = content
        };
        PostList.Add(newPost);
    }

    public void AddImagePostWithText(string content, string imageURL, Visibility visibility)
    {
        ImagePostWithText newPost = new ImagePostWithText
        {
            Author = this,
            PostVisibility = visibility,
            Content = content,
            ImageURL = imageURL
        };
        PostList.Add(newPost);
    }

    public void AddVideoPostWithText(string content, string videoURL, Visibility visibility)
    {
        VideoPostWithText newPost = new VideoPostWithText
        {
            Author = this,
            PostVisibility = visibility,
            Content = content,
            VideoURL = videoURL
        };
        PostList.Add(newPost);
    }

    public void AddImagePostWithTextAndFoodShopping
    (string content, string imageURL, string foodnameshopping, float foodpriceshopping, Visibility visibility)
    {
        ImagePostWithTextAndFoodShopping newPost = new ImagePostWithTextAndFoodShopping
        {
            Author = this,
            PostVisibility = visibility,
            Content = content,
            ImageURL = imageURL,
            FoodNameShopping = foodnameshopping,
            FoodPriceShopping = foodpriceshopping
        };
        PostList.Add(newPost);
    }

    public void AddVideoPostWithTextAndFoodShopping
    (string content, string videoURL, string foodnameshopping, float foodpriceshopping, Visibility visibility)
    {
        VideoPostWithTextAndFoodShopping newPost = new VideoPostWithTextAndFoodShopping
        {
            Author = this,
            PostVisibility = visibility,
            Content = content,
            VideoURL = videoURL,
            FoodNameShopping = foodnameshopping,
            FoodPriceShopping = foodpriceshopping
        };
        PostList.Add(newPost);
    }
}

public class TastyJourneyApp
{
    private Dictionary<string, User> _userDictionary = new Dictionary<string, User>();

    public void ShowUserFeed(User viewer)
    {
        int MAX_POST_PER_USER = 5;
        int MAX_POST_AMOUNT = 20;

        List<Post> postToDisplayList = new List<Post>();
        List<User> followList = viewer.GetFollowList();
        foreach (User user in followList)
        {
            if (postToDisplayList.Count >= MAX_POST_AMOUNT)
            {
                break;
            }

            int postCount = 0;
            foreach (Post post in user.FetchPublicPost())
            {
                if (postCount >= MAX_POST_PER_USER || postToDisplayList.Count >= MAX_POST_AMOUNT)
                {
                    break;
                }
                postToDisplayList.Add(post);
                ++postCount;
            }
        }
        Console.WriteLine();
        Console.WriteLine($"⊱┄┄┄ Show {viewer.Username} feed ┄┄┄⊰");
        Console.WriteLine();
        foreach (Post post in postToDisplayList)
        {
            post.Display();
            Console.WriteLine();
        }
        Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
    }

    public void ShowUserProfile(User userToDisplay)
    {
        List<Post> postToDisplayList = userToDisplay.FetchPublicPost();
        Console.WriteLine();
        Console.WriteLine($"⊱┄┄┄ Show {userToDisplay.Username} profile ┄┄┄⊰");
        Console.WriteLine();
        foreach (Post post in postToDisplayList)
        {
            post.Display();
            Console.WriteLine();
        }
        Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
    }

    public void ShowUserFeedOfFollowedUsers(User viewer)
    {
        List<User> followingList = viewer.GetFollowList();
        Console.WriteLine();
        Console.WriteLine($"Feed of users that {viewer.Username} is following:");
        foreach (User followingUser in followingList)
        {
            ShowUserFeed(followingUser);
        }
    }

    public void RegisterUser(string userName)
    {
        Console.WriteLine();
        Console.WriteLine($"⊱┄┄┄ Register ┄┄┄⊰");
        Console.WriteLine();
        if (_userDictionary.ContainsKey(userName))
        {
            Console.WriteLine($"Username @{userName} already registered");
            Console.WriteLine();
            Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
            return;
        }
        User newUser = new User { Username = userName };
        _userDictionary.Add(userName, newUser);
        Console.WriteLine($"Register user @{newUser.Username}");
        Console.WriteLine();
        Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
    }

    public void SearchOtherUser(string userToSearch)
    {
        Console.WriteLine();
        Console.WriteLine($"⊱┄┄┄ Search for user ┄┄┄⊰");
        Console.WriteLine();
        Console.WriteLine($"Finding user @{userToSearch} ...");
        foreach (var searchuser in _userDictionary)
        {
            if (_userDictionary.ContainsKey(userToSearch))
            {
                Console.WriteLine($"Found user @{userToSearch}");
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                return;
            }
        }
        Console.WriteLine($"Not found user @{userToSearch}");
        Console.WriteLine();
        Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
    }
}
public class InterfaceTastyJourneyApp
{

    public void Menu(User user, FoodInventory foodInventory, TastyJourneyApp tastyjourneyApp)
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to TastyJourney App!");
        Console.WriteLine($"Hello, {user.Username}!");
        Console.WriteLine("Menu:");
        Console.WriteLine("1. View your profile");
        Console.WriteLine("2. Make a new post");
        Console.WriteLine("3. View your feed");
        Console.WriteLine("4. View feed of users you follow");
        Console.WriteLine("5. Search for Food");
        Console.WriteLine("6. Search for User");
        Console.WriteLine("7. Log out");
        Console.Write("Please choose: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                tastyjourneyApp.ShowUserProfile(user);
                break;

            case "2":
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                AddPostFromUserInput(user, foodInventory, tastyjourneyApp);
                break;

            case "3":
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                tastyjourneyApp.ShowUserFeed(user);
                break;

            case "4":
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                tastyjourneyApp.ShowUserFeedOfFollowedUsers(user);
                break;

            case "5":
                Console.Write("Enter food to search: ");
                string foodToSearch = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                foodInventory.SearchFoodName(foodToSearch);
                break;

            case "6":
                Console.Write("Enter user to search: ");
                string userToSearch = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                tastyjourneyApp.SearchOtherUser(userToSearch);
                break;

            case "7":
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                Console.WriteLine();
                Console.WriteLine("Logged out successfully");
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                break;

            default:
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                Console.WriteLine();
                Console.WriteLine("Please choose a valid option");
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                break;
        }
    }

    public void AddPostFromUserInput(User user, FoodInventory foodInventory, TastyJourneyApp tastyjourneyApp)
    {
        string visibilityInput;
        tastyjourneyApp.ShowUserProfile(user);
        Console.WriteLine();
        Console.WriteLine($"Hello, {user.Username}! What's on your mind?");
        Console.WriteLine("1. Text Post");
        Console.WriteLine("2. Image Post with Text");
        Console.WriteLine("3. Video Post with Text");
        Console.WriteLine("4. Image Post with Text and add Food Shopping");
        Console.WriteLine("5. Video Post with Text and add Food Shopping");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter your text: ");
                string textContent = Console.ReadLine();
                Console.WriteLine("Please choose visibility");
                Console.WriteLine("1. Private");
                Console.WriteLine("2. Public");
                Console.Write("Choose an option: ");
                visibilityInput = Console.ReadLine();
                if (visibilityInput == "1" || visibilityInput == "2")
                {
                    Visibility visibility = (visibilityInput == "1") ? Visibility.PRIVATE : Visibility.PUBLIC;
                    user.AddTextPost(textContent, visibility);
                    Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                    Console.WriteLine();
                    tastyjourneyApp.ShowUserProfile(user);
                }
                else
                {
                    Console.WriteLine("Invalid visibility input");
                    Console.WriteLine("Please choose 1 or 2");
                }
                break;

            case "2":
                Console.Write("Enter your text: ");
                string imageContent = Console.ReadLine();
                Console.Write("Enter the image URL: ");
                string imageURL = Console.ReadLine();
                Console.WriteLine("Please choose visibility");
                Console.WriteLine("1. Private");
                Console.WriteLine("2. Public");
                Console.Write("Choose an option: ");
                visibilityInput = Console.ReadLine();
                if (visibilityInput == "1" || visibilityInput == "2")
                {
                    Visibility visibility = (visibilityInput == "1") ? Visibility.PRIVATE : Visibility.PUBLIC;
                    user.AddImagePostWithText(imageContent, imageURL, visibility);
                    Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                    Console.WriteLine();
                    tastyjourneyApp.ShowUserProfile(user);
                }
                else
                {
                    Console.WriteLine("Invalid visibility input");
                    Console.WriteLine("Please choose 1 or 2");
                }
                break;

            case "3":
                Console.Write("Enter your text: ");
                string videoContent = Console.ReadLine();
                Console.Write("Enter the video URL: ");
                string videoURL = Console.ReadLine();
                Console.WriteLine("Please choose visibility.");
                Console.WriteLine("1. Private");
                Console.WriteLine("2. Public");
                Console.Write("Choose an option: ");
                visibilityInput = Console.ReadLine();
                if (visibilityInput == "1" || visibilityInput == "2")
                {
                    Visibility visibility = (visibilityInput == "1") ? Visibility.PRIVATE : Visibility.PUBLIC;
                    user.AddVideoPostWithText(videoContent, videoURL, visibility);
                    Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                    Console.WriteLine();
                    tastyjourneyApp.ShowUserProfile(user);
                }
                else
                {
                    Console.WriteLine("Invalid visibility input");
                    Console.WriteLine("Please choose 1 or 2");
                }
                break;

            case "4":
                Console.Write("Enter your text: ");
                string imageContentWithFoodShopping = Console.ReadLine();
                Console.Write("Enter the image URL: ");
                string imageURLWithFoodShopping = Console.ReadLine();
                Console.WriteLine("Add a food shopping button");
                bool check_1 = true;
                while (check_1)
                {
                    Console.Write("Write food: ");
                    string foodnameshopping = Console.ReadLine();
                    foodInventory.Checkfood(foodnameshopping);
                    if (check_1 == foodInventory.Checkfood(foodnameshopping))
                    {
                        Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                        foodInventory.SearchFoodName(foodnameshopping);
                        float foodpriceshopping = foodInventory.GetFoodprice(foodnameshopping);
                        while (true)
                        {
                            Console.WriteLine("Please choose visibility");
                            Console.WriteLine("1. Private");
                            Console.WriteLine("2. Public");
                            Console.Write("Choose an option: ");
                            visibilityInput = Console.ReadLine();
                            if (visibilityInput == "1" || visibilityInput == "2")
                            {
                                Visibility visibility = (visibilityInput == "1") ? Visibility.PRIVATE : Visibility.PUBLIC;
                                user.AddImagePostWithTextAndFoodShopping(imageContentWithFoodShopping, imageURLWithFoodShopping, foodnameshopping, foodpriceshopping, visibility);
                                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                                Console.WriteLine();
                                tastyjourneyApp.ShowUserProfile(user);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid visibility input");
                                Console.WriteLine("Please choose 1 or 2");
                                continue;
                            }
                        }
                    }
                    else if (check_1 != foodInventory.Checkfood(foodnameshopping))
                    {
                        Console.WriteLine($"Not found: {foodnameshopping}");
                        Console.WriteLine($"Please write food again");
                        continue;
                    }
                    break;
                }
                break;

            case "5":
                Console.Write("Enter your text: ");
                string videoContentWithFoodShopping = Console.ReadLine();
                Console.Write("Enter the video URL: ");
                string videoURLWithFoodShopping = Console.ReadLine();
                Console.WriteLine("Add a food shopping button");
                bool check_2 = true;
                while (check_2)
                {
                    Console.Write("Write food: ");
                    string foodnameshopping = Console.ReadLine();
                    foodInventory.Checkfood(foodnameshopping);
                    if (check_2 == foodInventory.Checkfood(foodnameshopping))
                    {
                        Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                        foodInventory.SearchFoodName(foodnameshopping);
                        float foodpriceshopping = foodInventory.GetFoodprice(foodnameshopping);
                        while (true)
                        {
                            Console.WriteLine("Please choose visibility");
                            Console.WriteLine("1. Private");
                            Console.WriteLine("2. Public");
                            Console.Write("Choose an option: ");
                            visibilityInput = Console.ReadLine();
                            if (visibilityInput == "1" || visibilityInput == "2")
                            {
                                Visibility visibility = (visibilityInput == "1") ? Visibility.PRIVATE : Visibility.PUBLIC;
                                user.AddVideoPostWithTextAndFoodShopping(videoContentWithFoodShopping, videoURLWithFoodShopping, foodnameshopping, foodpriceshopping, visibility);
                                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                                Console.WriteLine();
                                tastyjourneyApp.ShowUserProfile(user);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid visibility input");
                                Console.Write("Please choose 1 or 2");
                                continue;
                            }
                        }
                    }
                    else if (check_2 != foodInventory.Checkfood(foodnameshopping))
                    {
                        Console.WriteLine($"Not found: {foodnameshopping}");
                        Console.WriteLine("Please write food again");
                        continue;
                    }
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                break;

            default:
                Console.WriteLine("Invalid choice.");
                Console.WriteLine("Please choose 1 or 2 or 3 or 4 or 5 or 6");
                Console.WriteLine();
                Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
                break;
        }
    }
    public void FoodShoppingFromUser(User user, FoodInventory foodInventory, TastyJourneyApp tastyjourneyApp)
    {
        tastyjourneyApp.ShowUserFeed(user);
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Do you want this food?");
            Console.WriteLine("Shipping cost is 20 baht for every item!");
            Console.Write("Y/N: ");
            string yesorno = Console.ReadLine();
            if (yesorno == "Y")
            {
                Console.WriteLine("Please input food from post");
                Console.Write("Write food: ");
                string foodnameshopping = Console.ReadLine();
                Button button = new Button();
                Console.WriteLine();
                button.SimulateWindow();
                while (true)
                {
                    Console.WriteLine("You can order no more than 10 food items at a time");
                    Console.Write("Input quantity: ");
                    int quantity = int.Parse(Console.ReadLine());
                    if ((quantity > 0) && (quantity <= 10))
                    {
                        Console.Write("Write food detail: ");
                        string foodDetail = Console.ReadLine();
                        Console.WriteLine("Shipping cost is 20 baht");
                        foodInventory.OrderFood(foodnameshopping, quantity, foodDetail);
                        float total = foodInventory.GetTotal();
                        while (true)
                        {
                            Console.WriteLine("Confirm your order");
                            Console.WriteLine("Is your order correct?");
                            Console.Write("Y/N: ");
                            yesorno = Console.ReadLine();
                            if (yesorno == "Y")
                            {
                                Console.WriteLine();
                                Console.WriteLine("┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄");
                                Console.WriteLine("⊱┄┄┄ Payment your Order ┄┄┄⊰");
                                Console.Write("Write amount you pay: ");
                                float pay = float.Parse(Console.ReadLine());
                                Console.WriteLine("┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄");
                                Console.WriteLine();
                                foodInventory.Payment(total, pay);
                                bool check_3 = true;
                                if (check_3 == foodInventory.Payment(total, pay))
                                {
                                    Console.WriteLine("Payment completed");
                                    Console.WriteLine("Wait to receive food items you ordered!");
                                }
                                else if (!check_3 == foodInventory.Payment(total, pay))
                                {
                                    Console.WriteLine("You paid incorrectly");
                                }
                            }
                            else if (yesorno == "N")
                            {
                                Console.WriteLine("Please order again");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid visibility input");
                                Console.Write("Please input Y or N");
                                continue;
                            }
                            break;
                        }
                    }
                    else if (quantity == 0)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                    break;
                }
            }
            else if (yesorno == "N")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input");
                Console.WriteLine("Please input Y or N");
                continue;
            }
            Console.WriteLine();
            Console.WriteLine("╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸╸");
            break;
        }

    }

}
public class Program
{
    public static void Main(string[] args)
    {
        //User Example
        User user1 = new User { Username = "antonlee", NumOfFollower = 0, NumOfFollowing = 0 };
        User user2 = new User { Username = "hayatosano", NumOfFollower = 0, NumOfFollowing = 0 };
        User user3 = new User { Username = "blade", NumOfFollower = 0, NumOfFollowing = 0 };

        //Show Register
        TastyJourneyApp tastyjourneyapp = new TastyJourneyApp();
        tastyjourneyapp.RegisterUser(user1.Username);
        tastyjourneyapp.RegisterUser(user2.Username);
        tastyjourneyapp.RegisterUser(user3.Username);
        tastyjourneyapp.RegisterUser(user3.Username); //registerUser ซ้ำ

        //Show Follow Other User And Show Follower , Following
        user1.FollowUser(user2);
        user2.FollowUser(user1);
        user3.FollowUser(user1);
        user3.FollowUser(user2);
        user1.FollowUser(user2); //follow ซ้ำ

        //Show Search User
        tastyjourneyapp.SearchOtherUser(user2.Username);
        tastyjourneyapp.SearchOtherUser("jangwonyoung"); //not found

        //Show Add food to the food inventory
        FoodInventory foodInventory = new FoodInventory();
        foodInventory.AddFoodInInventory("Milk Tea", 60);
        foodInventory.AddFoodInInventory("Strawberry Cheesecake", 150);
        foodInventory.AddFoodInInventory("Pie", 100);
        foodInventory.AddFoodInInventory("Birthday Cake", 500.50f);
        foodInventory.AddFoodInInventory("Pudding", 120);
        foodInventory.AddFoodInInventory("Chabu", 267);
        foodInventory.AddFoodInInventory("KFC", 89);
        foodInventory.AddFoodInInventory("Pizza", 299);
        foodInventory.AddFoodInInventory("Pizza", 299); //input food ซ้ำ

        //Show Search Food In Inventory
        foodInventory.SearchFoodName("Strawberry Cheesecake");
        foodInventory.SearchFoodName("Pudding");
        foodInventory.SearchFoodName("Roti"); //not found

        //Show Create Post
        user3.AddTextPost("Of five people, three must pay a price...", Visibility.PUBLIC);
        user1.AddImagePostWithText("Happyyyyy😀", "antonsmile", Visibility.PRIVATE); //private
        user2.AddVideoPostWithText("With Anton🙌", "Dance", Visibility.PUBLIC);
        user1.AddImagePostWithTextAndFoodShopping("Strawberry Cheesecake🍓🍰 100/10 click it!", "StrawberryCheesecake", "Strawberry Cheesecake", 150, Visibility.PUBLIC);
        user2.AddVideoPostWithTextAndFoodShopping("I love pudding🍮", "Pudding", "Pudding", 120, Visibility.PUBLIC);

        //Show User Profile
        tastyjourneyapp.ShowUserProfile(user1);
        tastyjourneyapp.ShowUserProfile(user2);
        tastyjourneyapp.ShowUserProfile(user3);

        //Show User Feed
        tastyjourneyapp.ShowUserFeed(user1);
        tastyjourneyapp.ShowUserFeed(user2);
        tastyjourneyapp.ShowUserFeed(user3);

        //Show Order Food
        InterfaceTastyJourneyApp interfaceTastyJourneyApp = new InterfaceTastyJourneyApp();
        interfaceTastyJourneyApp.FoodShoppingFromUser(user1, foodInventory, tastyjourneyapp);
        interfaceTastyJourneyApp.FoodShoppingFromUser(user2, foodInventory, tastyjourneyapp);

        //Show Input Menu And Create Post
        interfaceTastyJourneyApp.Menu(user1, foodInventory, tastyjourneyapp);
        interfaceTastyJourneyApp.Menu(user1, foodInventory, tastyjourneyapp);
        interfaceTastyJourneyApp.Menu(user1, foodInventory, tastyjourneyapp);
        interfaceTastyJourneyApp.Menu(user1, foodInventory, tastyjourneyapp);
        interfaceTastyJourneyApp.Menu(user1, foodInventory, tastyjourneyapp);
        interfaceTastyJourneyApp.Menu(user1, foodInventory, tastyjourneyapp);
        interfaceTastyJourneyApp.Menu(user1, foodInventory, tastyjourneyapp);
        interfaceTastyJourneyApp.Menu(user1, foodInventory, tastyjourneyapp);
    }
}
