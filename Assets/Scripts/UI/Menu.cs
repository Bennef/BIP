using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Scripts.UI
{
    public class Menu : MonoBehaviour
    {
        public enum NavigationMode { Vertical, Horizontal }

        public NavigationMode navigationMode = NavigationMode.Vertical;

        // Circular, meaning if it reaches the end, does it go to the start? and vice versa.
        public bool circular;

        // All selectable UI elements inherit from UnityEngine.UI.Selectable - using this type in the list allows for mixing of different elements
        private List<Selectable> menuElements = null;

        void Start() => BuildNavigation();

        void OnEnable() => BuildNavigation();

        void BuildNavigation()
        {
            // grab all Selectables that are children of the gameObject this is attatched to.
            FindMenuElements();
            // Set up the navigation for a circullar list following the order of objects in hierarchy and ignoring non-interactable elements
            CreateNavigation();
            // Set the first interactable element as selected in the EventSystem
            ResetSelection();
        }

        void FindMenuElements()
        {
            // Populate the menuElements list.
            menuElements = new List<Selectable>();

            foreach (Transform child in gameObject.transform)
            {
                Selectable selectable = child.GetComponent<Selectable>();
                if (selectable != null)
                {
                    menuElements.Add(selectable);
                }
            }
        }

        void CreateNavigation()
        {
            for (int i = 0; i < menuElements.Count; i++)
            {
                Selectable next = null;
                Selectable previous = null;
                FindNeighbours(i, ref next, ref previous);

                // Create and initialise new Navigation object, then assign it to the current Selectable
                // Navigation is built into the Unity UI system.
                Navigation navigation;
                switch (navigationMode)
                {
                    case NavigationMode.Vertical:
                        navigation = new Navigation() { mode = Navigation.Mode.Explicit, selectOnUp = previous, selectOnDown = next };
                        break;

                    case NavigationMode.Horizontal:
                        navigation = new Navigation() { mode = Navigation.Mode.Explicit, selectOnLeft = previous, selectOnRight = next };
                        break;

                    default:
                        navigation = new Navigation() { mode = Navigation.Mode.Explicit };
                        break;
                }

                menuElements[i].navigation = new Navigation() { mode = Navigation.Mode.None };
                menuElements[i].navigation = navigation;
            }
        }

        void FindNeighbours(int i, ref Selectable next, ref Selectable previous)
        {
            // Set up which objects can be navigated to from currently selected.
            int nextIndex = i + 1;
            int previousIndex = i - 1;
            if (circular)
            {
                nextIndex %= menuElements.Count;
                previousIndex = previousIndex < 0 ? menuElements.Count - 1 : previousIndex;

                next = menuElements[nextIndex];
                previous = menuElements[previousIndex];
            }
            else
            {
                if (nextIndex < menuElements.Count)
                    next = menuElements[nextIndex];
                if (previousIndex >= 0)
                    previous = menuElements[previousIndex];
            }
        }

        public void ResetSelection()
        {
            // Set currently selected to null
            EventSystem.current.SetSelectedGameObject(null, new BaseEventData(EventSystem.current));
            // Select the initial value whenever we reset.
            if (menuElements.Count > 0)
            {
                //EventSystem.current.SetSelectedGameObject(menuElements[0].gameObject, new BaseEventData(EventSystem.current));
            }
        }
    }
}