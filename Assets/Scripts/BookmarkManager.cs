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
    



    // Start is called before the first frame update
    void Start()
    {
        bookmarkCollection.SetActive(false);
        mainCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();
        bmPanel.SetActive(false);
        // hard-coded List
        // Bookmark top_bookmark = new Bookmark("top view", new Vector3(6.3f, 31f, 7.4f));
        bookmarkList =  new List<Bookmark>(GetComponentsInChildren<Bookmark>());
        // bookmarkList.Add(top_bookmark);
        // Title.text = bookmarkList[0].GetPosition().ToString();

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
        if (showBookmarks) {bookmarkCollection.SetActive(true);}
        else { bookmarkCollection.SetActive(false);}
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
            GameObject newBookmark = Instantiate (bm_prefab, mainCamera.gameObject.transform.position, Quaternion.identity);
            newBookmark.transform.SetParent(BookmarkParent);
            newBookmark.GetComponent<Bookmark>().SetPosition(mainCamera.gameObject.transform.position);
            bookmarkList.Add(newBookmark.GetComponent<Bookmark>());
            bmPanel.SetActive(false);

        }

    }
}
