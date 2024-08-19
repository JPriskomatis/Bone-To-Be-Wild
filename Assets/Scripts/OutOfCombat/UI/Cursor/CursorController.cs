using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CursorController : MonoBehaviour
    {
        public Texture2D cursor;
        public Texture2D cursorUpdate;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)){
                Cursor.SetCursor(cursorUpdate, Vector2.zero, 0);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Cursor.SetCursor(cursor, Vector2.zero, 0);
            }

        }

    }
}

