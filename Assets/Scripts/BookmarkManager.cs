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

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        mainCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();
        
        // hard-coded List
        Bookmark top_bookmark = new Bookmark("top view", new Vector3(6.3f, 31f, 7.4f));
        bookmarkList.Add(top_bookmark);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleShow()
    {
        showBookmarks = !showBookmarks;
        if (showBookmarks) {this.gameObject.SetActive(true);}
        else { this.gameObject.SetActive(false);}
    }

    public void TeleportToBookmark()
    {
        Title.text = "Button is touched"; 
        player.transform.position = new Vector3(6.3f,31f,7.4f);
        // if (currentBookmark!= null) {player.transform.position = currentBookmark.GetPosition();}
        // else if (bookmarkList[bookmarkList.Count -1]!= null) {player.transform.position = bookmarkList[bookmarkList.Count -1].GetPosition();}
    }
}
