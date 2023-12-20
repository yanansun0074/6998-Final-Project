using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;

public class BookmarkManager : MonoBehaviour
{
    private bool showBookmarks = false;
    
    private Camera mainCamera;
    public string currentBookmark = null;
    protected string pastBookmark = null;
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

    // for GridObject update
    private IEnumerator coroutine;

    public GameObject miniatureCam;

    public bool past_dirty;

    // public Quaternion bmRotate;

    



    // Start is called before the first frame update
    void Start()
    {
        // By default bookmarks are not shown in map
        bookmarkCollection.SetActive(false);
        bmPanel.SetActive(false);
        scrollview.SetActive(false);

        mainCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();

        // initialize list from Bookmark Collection
        bookmarkList =  new List<Bookmark>(bookmarkCollection.GetComponentsInChildren<Bookmark>());
        
        // Initialize bookmar scrollview
        coroutine = InvokeUpdateCollection();
        StartCoroutine(coroutine);

        PopulateList();

        past_dirty = true;

    }

    // Update is called once per frame
    void Update()
    {
        positionText.text = mainCamera.gameObject.transform.position.ToString();

        // if currentbookmark changed
        if (past_dirty)
        {
            if (!currentBookmark.Equals(pastBookmark))
            {
                CleanBookmark();
            }
            past_dirty = false;

        }


    }

    public void CleanBookmark()
    {
        foreach (Bookmark bm in bookmarkList)
        {
            if (bm.GetName().Equals(currentBookmark))
            {
                bm.selected = true;
            }
            else {bm.selected = false;}
        }
        pastBookmark = currentBookmark;
    }


    // Show all existing bookmarks or not
    public void ToggleShow()
    {
        showBookmarks = !showBookmarks;
        if (showBookmarks) 
        {
            bookmarkCollection.SetActive(true);
            scrollview.SetActive(true);
            miniatureCam.GetComponent<MiniatureCameraScript>().overview = true;
            
        }
        else { 
            bookmarkCollection.SetActive(false);
            scrollview.SetActive(false);
            miniatureCam.GetComponent<MiniatureCameraScript>().overview = false;
            Title.text = "Miniature Map";
        }
    }

    // teleport to selected bookmark
    public void TeleportToBookmark()
    {
        // if a bookmark is selected, teleport to that one
        if (currentBookmark != "") 
        {
            foreach (Bookmark bm in bookmarkList)
            {
                if (bm.GetName() == currentBookmark)
                {
                    player.transform.position = bm.GetPosition();
                    Title.text = "Miniature Map";
                    break;
                }
            }

        }
        // if no bookmark is selected, teleport to the latest one
        else if (bookmarkList[bookmarkList.Count -1] != null) 
        {
            player.transform.position = bookmarkList[bookmarkList.Count -1].GetPosition();
            Title.text = "Miniature Map";
            // Title.text = "Go to the latest bookmark " + bookmarkList[bookmarkList.Count -1].GetPosition().ToString();

        }
        
        else{Title.text = "no bookmark saved";}
    }

    public void AddBookmark()
    {
        StartCoroutine(coroutine);
        AddBookmark_Coroutine();
    }
    // Add new bookmark
    public void AddBookmark_Coroutine()
    {               
        if (bm_prefab)
        {
            // Create bookmark
            Vector3 camPos = mainCamera.gameObject.transform.position;
            
            GameObject newBookmark = Instantiate (bm_prefab, new Vector3(0,0,0), Quaternion.identity);
            newBookmark.transform.SetParent(BookmarkParent);
            Bookmark bm = newBookmark.GetComponent<Bookmark>();
            // Set bookmark.name = position
            bm.SetName(mainCamera.gameObject.transform.position.ToString());
            // Set bookmark.position
            bm.SetPosition(mainCamera.gameObject.transform.position);
            // Set bookmark.markPin
            bm.SetPin(newBookmark);
            // Add to list
            bookmarkList.Add(bm);
            // Set name of game object
            newBookmark.name = bm.GetName();

            // Add to scrollview
            GameObject created_btn = (GameObject) Instantiate(bmButton_prefab, selectables.transform); 
            // StartCoroutine(coroutine);
            selectables.GetComponent<GridObjectCollection>().UpdateCollection();
          
            // Change name of button
            created_btn.transform.Find("IconAndText").GetComponentInChildren<TMP_Text>().text = bm.GetName();
            
            // Close addBookmark panel after finished
            bmPanel.SetActive(false);

        }
        

    }

    // When initialized, update grid view
    public void PopulateList()
    {
        foreach (Bookmark bm in bookmarkList)
        {
            // Create button
            GameObject created_btn = (GameObject) Instantiate(bmButton_prefab, selectables.transform);
            
            // Change text of button
            if (bm.GetName() != null)
            {
                // Debug.Log("Added " + bm.GetName());
                created_btn.transform.Find("IconAndText").GetComponentInChildren<TMP_Text>().text = bm.GetName();

            }
            else {
                // Debug.Log("Added default position as name");
                created_btn.transform.Find("IconAndText").GetComponentInChildren<TMP_Text>().text = bm.GetPosition().ToString();

            }
        }
    }

    private IEnumerator InvokeUpdateCollection()
{
    yield return new WaitForEndOfFrame();
    selectables.GetComponent<GridObjectCollection>().UpdateCollection();
}

}
