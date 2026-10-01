using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    public float moveSpeed = 3;
    float x_input;
    float y_input;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region Health_variables
    public float maxHealth = 5;
    public float currHealth = 5;
    #endregion


    #region Animation_components
    Animator anim;
    #endregion

    #region Unity_functions
    private void Awake() {
        
    }
    private void Update() {
        
        
    }
    #endregion



    
    
}
