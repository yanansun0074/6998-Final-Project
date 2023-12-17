using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookmarkManager : MonoBehaviour
{
    private bool showBookmarks = false;
    
    private Camera mainCamera;
    protected Bookmark currentBookmark;
    protected List<Bookmark> bookmarkList;

    public GameObject player;
    public TextMeshPro Title;
    public GameObject bookmarkCollection;

    // Bookmark pin 3D prefab
    public GameObject bm_prefab;
    // Set parent
    public Transform BookmarkParent;
    // Add bookmark UI
    public GameObject bmPanel;
    // how Bookmark current position
    public TextMeshPro positionText;

    // bookmark scrollview buttons
    [SerializeField]
    public GameObject bmButton_prefab;
    // bookmark list view
    [SerializeField]
    public GameObject selectables;
    // bookmark scrollview
    public GameObject scrollview;
    



    // Start is called before the first frame update
    void Start()
    {
        bookmarkCollection.SetActive(false);
        mainCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();
        bmPanel.SetActive(false);

        // initialize list
        bookmarkList =  new List<Bookmark>(GetComponentsInChildren<Bookmark>());
        // By default add top view
        Bookmark top_bookmark = new Bookmark("top view", new Vector3(6.3f, 31f, 7.4f));
        bookmarkList.Add(top_bookmark);
        
        // Initialize bookmar scrollview
        scrollview.SetActive(false);
        PopulateList();

    }

    // Update is called once per frame
    void Update()
    {
        positionText.text = mainCamera.gameObject.transform.position.ToString();
    }

    // Show all existing bookmarks or not
    public void ToggleShow()
    {
        showBookmarks = !showBookmarks;
        if (showBookmarks) 
        {
            bookmarkCollection.SetActive(true);
            scrollview.SetActive(true);
            
        }
        else { 
            bookmarkCollection.SetActive(false);
            scrollview.SetActive(false);
        }
    }

    // teleport to selected bookmark
    public void TeleportToBookmark()
    {
        // if a bookmark is selected, teleport to that one
        if (currentBookmark!= null) 
        {
            Title.text = "Go to selected bookmark!";
            player.transform.position = currentBookmark.GetPosition();
        }
        // if no bookmark is selected, teleport to the latest one
        else if (bookmarkList[bookmarkList.Count -1]!= null) 
        {
            Title.text = "Go to the latest bookmark";
            player.transform.position = bookmarkList[bookmarkList.Count -1].GetPosition();}
        
        else{Title.text = "no bookmark saved";}
    }

    // Add new bookmark
    public void AddBookmark()
    {
        if (bm_prefab)
        {
            // Create bookmark and translate its position
            GameObject newBookmark = Instantiate (bm_prefab, mainCamera.gameObject.transform.position, Quaternion.identity);
            newBookmark.transform.SetParent(BookmarkParent);
            Bookmark bm = newBookmark.GetComponent<Bookmark>();
            bm.SetPosition(mainCamera.gameObject.transform.position);
            
            // Add to list
            bookmarkList.Add(bm);

            // Add to scrollview
            GameObject created_btn = (GameObject) Instantiate(bmButton_prefab, selectables.transform);            
            // Change text of button
            if (bm.GetName()!=null) {created_btn.GetComponentInChildren<TextMeshProUGUI>().text = bm.GetName();}
            else {created_btn.GetComponentInChildren<TextMeshProUGUI>().text = "New Bookmark";}

            // Close addBookmark panel after finished
            bmPanel.SetActive(false);

        }

    }

    public void PopulateList()
    {
        foreach (Bookmark bm in bookmarkList)
        {
            // Create button
            GameObject created_btn = (GameObject) Instantiate(bmButton_prefab, selectables.transform);
            
            // Change text of button
            // TextMeshProUGUI btn_txt = created_btn.GetComponentInChildren<TextMeshProUGUI>();
            created_btn.GetComponentInChildren<TextMeshProUGUI>().text = bm.GetName();
        }
    }
}
