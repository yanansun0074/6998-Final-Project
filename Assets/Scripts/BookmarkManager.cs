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
        
        // By default add top view
        // Bookmark top_bookmark = new Bookmark("top view", new Vector3(6.3f, 31f, 7.4f));
        // bookmarkList.Add(top_bookmark);
        
        // Initialize bookmar scrollview
        coroutine = InvokeUpdateCollection();
        StartCoroutine(coroutine);

        PopulateList();

    }

    // Update is called once per frame
    void Update()
    {
        positionText.text = mainCamera.gameObject.transform.position.ToString();

        // if currentbookmark changed
        if (currentBookmark != pastBookmark && currentBookmark != "")
        {
            Title.text = currentBookmark + " is selected";
            if (pastBookmark != "")
            {
                GameObject.Find(pastBookmark).GetComponent<Bookmark>().selected = false;
            }
            GameObject.Find(currentBookmark).GetComponent<Bookmark>().selected = true;
            pastBookmark = currentBookmark;
        }

        // InvokeUpdateCollection();

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
                    // Title.text = "Go to selected bookmark! " + bm.GetPosition().ToString();
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
            GameObject newBookmark = Instantiate (bm_prefab, mainCamera.gameObject.transform.position, Quaternion.identity);
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

    public void PopulateList()
    {
        foreach (Bookmark bm in bookmarkList)
        {
            // Create button
            GameObject created_btn = (GameObject) Instantiate(bmButton_prefab, selectables.transform);
            
            // Change text of button
            // TextMeshProUGUI btn_txt = created_btn.GetComponentInChildren<TextMeshProUGUI>();
            if (bm.GetName() != null)
            {
                // created_btn.GetComponentInChildren<TextMeshProUGUI>().text = bm.GetName();
                Debug.Log("Added " + bm.GetName());
                created_btn.transform.Find("IconAndText").GetComponentInChildren<TMP_Text>().text = bm.GetName();

            }
            else {
                Debug.Log("Added default position as name");
                // created_btn.GetComponentInChildren<TextMeshProUGUI>().text = bm.GetPosition().ToString();
                created_btn.transform.Find("IconAndText").GetComponentInChildren<TMP_Text>().text = bm.GetPosition().ToString();

            }
        }

        // InvokeUpdateCollection();
    }

    private IEnumerator InvokeUpdateCollection()
{
    yield return new WaitForEndOfFrame();
    selectables.GetComponent<GridObjectCollection>().UpdateCollection();
}

}
