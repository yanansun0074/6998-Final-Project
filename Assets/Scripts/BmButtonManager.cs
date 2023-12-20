using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class BmButtonManager : MonoBehaviour
{
    private BookmarkManager bManager;
    // Start is called before the first frame update
    void Start()
    {
        
        bManager = GameObject.Find("BookmarkManager").GetComponent<BookmarkManager>();
    }

    public void UpdateSelected()
    {
    
        bManager.currentBookmark = this.transform.Find("IconAndText").GetComponentInChildren<TMP_Text>().text;
        bManager.past_dirty = true;
        
    }

}
