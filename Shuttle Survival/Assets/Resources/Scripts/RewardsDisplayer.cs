using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsDisplayer : MonoBehaviour
{
    public static RewardsDisplayer rewardsDisplayer;
    [SerializeField] GameObject rewardsUIHolder;
    [SerializeField] ItemUI itemUIPrefab;
    List<ItemUI> currentRewards = new List<ItemUI>();
    [SerializeField] Transform rewardsGrid;
    [SerializeField] GameObject closeButton;
    private void Awake()
    {
        if(RewardsDisplayer.rewardsDisplayer == null)
        {
            RewardsDisplayer.rewardsDisplayer = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveRewardsToDisplay(ItemStack[] rewardsToDisplay, bool displayCloseButton)
    {
        rewardsUIHolder.SetActive(true);
        ClearPreviousListOfRewards();
        closeButton.SetActive(displayCloseButton);
        foreach (ItemStack itemStack in rewardsToDisplay)
        {
            ItemUI newReward = Instantiate(itemUIPrefab, rewardsGrid);
            newReward.GetComponent<RectTransform>().pivot = new Vector2(0.45f, 2.6f);
            newReward.SetupItemUI(itemStack.Item.icon, itemStack.Quantite, itemStack.Item);
            currentRewards.Add(newReward);
        }
    }

    private void ClearPreviousListOfRewards()
    {
        foreach (ItemUI itemUI in currentRewards)
        {
            Destroy(itemUI.gameObject);
        }
        currentRewards.Clear();
    }

    public void CloseRewardsUI()
    {
        rewardsUIHolder.SetActive(false);
    }
}
