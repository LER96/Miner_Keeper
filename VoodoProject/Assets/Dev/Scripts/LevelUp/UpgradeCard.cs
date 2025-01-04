using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [Header("Card attachement")]
    [SerializeField] Transform _parent;
    [SerializeField] Transform _attachTo;

    [Header("Card Variables")]
    [SerializeField] Animator _animator;
    [SerializeField] Button _upgradeBTN;
    [SerializeField] Image _upgradeImg;
    [SerializeField] TMP_Text _upgradeName;
    [SerializeField] TMP_Text _description;

    private UpgradeSO _upgradeData;

    public void SetData(UpgradeSO upgrade)
    {
        _upgradeData = upgrade;
        _upgradeBTN.onClick.AddListener(_upgradeData.SetUpgrade);
        _upgradeBTN.onClick.AddListener(Click);
        _upgradeImg.sprite = upgrade.UpgradeImg;
        _upgradeName.text = $"{upgrade.UpgradeName}";
        _description.text = $"{upgrade.Description}";
    }

    void Click()
    {
        Time.timeScale = 1;
        _animator.Play("Clicked");
    }

    public void PopDown()
    {
        _animator.Play("PopDown");
    }

    public void DisableCard()
    {
        UpgradeManager.Instance.UpgradeHandler.DisableCards();
    }

    public void SetToOrigin()
    {
        transform.SetParent(_parent);
    }

    public void Attach()
    {
        transform.SetParent(_attachTo);
    }
}
