using UnityEngine;
using System.Collections;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string _title;
    [TextArea]
    [SerializeField] private string _tutorialInfo;

    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private float _typeDelay = 0.02f;
    [SerializeField] private float _selfDestructTime = 5f;
    private bool _destroySelf = false;

    private string _currText;    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _titleText.SetText(_title);
            StartCoroutine(TypeInfo(_tutorialInfo));
        }
    }

    IEnumerator TypeInfo(string info)
    {
        for (int i = 0; i <= _tutorialInfo.Length; i++)
        {
            _currText = info.Substring(0, i);
            _infoText.SetText(_currText);
            yield return new WaitForSeconds(_typeDelay);

            if(i == _tutorialInfo.Length)
            {
                yield return new WaitForSeconds(_selfDestructTime);
                _destroySelf = true;
            }
        }
    }

    private void Update()
    {
        if (_destroySelf == true)
        {
            FadeText();
            Destroy(this.gameObject);
        }
    }

    private void FadeText()
    {
        _titleText.SetText(" ");
        _infoText.SetText(" ");
    }
}
