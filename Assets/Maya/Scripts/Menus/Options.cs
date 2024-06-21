using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public void OnExit()
    {
        UIManager.Instance.UnloadSceneAsync(Scenes.Options);
    }

    #region Tab Management

    [Header("Tabs")]
    [SerializeField] private Image[] tabButtons;
    [SerializeField] private GameObject[] tabContent;
    [SerializeField] private int firstSelectedTab = 0;
    private int selectedTab;

    [Space]

    [SerializeField] private Color selectedButton;
    [SerializeField] private Color deselectedButton;

    private void Awake()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        for (int i = 0; i < tabContent.Length; i++)
            tabContent[i].SetActive(true);

        yield return new WaitForSeconds(1f);

        // Select first Tab
        for (int i = 0; i < tabContent.Length; i++)
        {
            if (i == firstSelectedTab) SelectTab(i);
            else DeselectTab(i);
        }
    }

    public void OnChangeTab(int _tab)
    {
        DeselectTab(selectedTab);
        SelectTab(_tab);
    }

    /// <summary>
    /// Activate Tab Content and set the color of the button to selected
    /// </summary>
    /// <param name="_tab"></param>
    private void SelectTab(int _tab)
    {
        selectedTab = _tab;
        tabButtons[_tab].color = selectedButton;
        tabContent[_tab].SetActive(true);
    }

    /// <summary>
    /// Deactivate the active Tab Content and set the color of its button to deselected
    /// </summary>
    /// <param name="_tab"></param>
    private void DeselectTab(int _tab)
    {
        tabContent[_tab].SetActive(false);
        tabButtons[_tab].color = deselectedButton;
    }

    #endregion
}
