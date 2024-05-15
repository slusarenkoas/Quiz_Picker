using System;
using System.Threading.Tasks;
using DG.Tweening;
using MessagePipe;
using Messages;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class CardView : MonoBehaviour, IPointerClickHandler, IDisposable
    {
        [SerializeField] private Image _card;

        private string _identifier;
        private RandomTargetProvider _randomTargetProvider;
        private bool _isThisCardTarget = false;

        private IDisposable _subscriber;
        private IPublisher<TargetCardFoundMessage> _targetCardFoundPublisher;

        public void Initialize(CardData cardData, 
            ISubscriber<TargetChosenMessage> targetChosenSubscriber,
            IPublisher<TargetCardFoundMessage> targetCardFoundPublisher)
        {
            _subscriber = targetChosenSubscriber.Subscribe(message => MarkTargetCard(message.Target));
            _targetCardFoundPublisher = targetCardFoundPublisher;

            _card.sprite = cardData.Sprite;
            _identifier = cardData.Identifier;
        }
        
        private void MarkTargetCard(string targetValue)
        {
            if (targetValue == _identifier)
            {
                _isThisCardTarget = true;
                return;
            }

            _isThisCardTarget = false;
        }

        public async void OnPointerClick(PointerEventData eventData)
        {
            Vector3 originalScale = transform.localScale;
            Vector3 targetScale = originalScale * 1.5f;
            
            if (_isThisCardTarget)
            {
                // Scale up with bounce effect
                _card.rectTransform.DOShakeScale(1);
                
                await Task.Delay(TimeSpan.FromSeconds(1f));
                
                _isThisCardTarget = false;
                _targetCardFoundPublisher.Publish(new TargetCardFoundMessage());
            }
            else
            {
                _card.rectTransform.DOShakeScale(1);
            }
        }

        public void Dispose()
        {
            _subscriber.Dispose();
        }
    }