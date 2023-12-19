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
        // SolverHandler sh = GameObject.Find("Menu").GetComponent<SolverHandler>();
        // sh.UpdateSolvers = true;
        // this.GetComponent<Button>().onClick.AddListener(sh.UpdateSolvers);
        // this.GetComponent<Button>().onClick.AddListener(RegisterSolver);
        bManager = GameObject.Find("BookmarkManager").GetComponent<BookmarkManager>();
    }

    public void UpdateSelected()
    {
        // bManager.currentBookmark = this.GetComponentInChildren<TextMeshProUGUI>().text;
        bManager.currentBookmark = this.transform.Find("IconAndText").GetComponentInChildren<TMP_Text>().text;
        // this.GetComponentInChildren<TextMeshProUGUI>().text = "selected";
        // this.transform.Find("IconAndText").GetComponentInChildren<TMP_Text>().text = "selected";

    }

}
