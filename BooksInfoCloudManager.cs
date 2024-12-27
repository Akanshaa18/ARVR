using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class BooksInfoCloudManager : MonoBehaviour
{
    private CloudRecoBehaviour mCloudRecoBehaviour;
    private bool mIsScanning = false;
    private string mTargetMetadata = "";

    // To store data from Metadata file
    string BookName, AuthorName, Description, Price, Rating, MoreInfoURL;

    public GameObject BooksInfoDataPanel; // Books Info UI Panel

    // Display the string data in the corresponding text field in application UI
    public TextMeshProUGUI BookNameData, AuthorNameData, DescriptionData, PriceData, RatingData, MoreInfoURLData;

    void Start()
    {
        BookNameData.text = "";
        AuthorNameData.text = "";
        DescriptionData.text = "";
        PriceData.text = "";
        RatingData.text = "";
        MoreInfoURLData.text = "";
        BooksInfoDataPanel.SetActive(false);

    }

    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    public void OnInitialized(TargetFinder targetFinder)
    {
        Debug.Log("Cloud Reco initialized");
    }
    public void OnInitError(TargetFinder.InitState initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }
    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());
    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;
        if (scanning)
        {
            // clear all known trackables
            var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
        }
    }

    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        TargetFinder.CloudRecoSearchResult cloudRecoSearchResult =
            (TargetFinder.CloudRecoSearchResult)targetSearchResult;

        Debug.Log(cloudRecoSearchResult.MetaData);

        string[] BookDetails = cloudRecoSearchResult.MetaData.Split('*');

        Debug.Log(BookDetails.Length);

        BookName = BookDetails[0];
        AuthorName = BookDetails[1];
        Description = BookDetails[2];
        Price = BookDetails[3];
        Rating = BookDetails[4];
        MoreInfoURL = BookDetails[5]; 

        // do something with the target metadata
        //mTargetMetadata = cloudRecoSearchResult.MetaData;


        // stop the target finder (i.e. stop scanning the cloud)
        mCloudRecoBehaviour.CloudRecoEnabled = false;

        DisplayData();
    }

    private void DisplayData()
    {
        BookNameData.text = BookName;
        AuthorNameData.text = AuthorName;
        DescriptionData.text = Description;
        PriceData.text = Price;
        RatingData.text = Rating;
        MoreInfoURLData.text = MoreInfoURL;
        BooksInfoDataPanel.SetActive(true);
    }

    public void ScanTarget()
    {

        if (!mIsScanning)
        {
            // Restart TargetFinder
            mCloudRecoBehaviour.CloudRecoEnabled = true;
            BookNameData.text = "";
            AuthorNameData.text = "";
            DescriptionData.text = "";
            PriceData.text = "";
            RatingData.text = "";
            MoreInfoURLData.text = "";
            BooksInfoDataPanel.SetActive(false);

        }
    }
}
