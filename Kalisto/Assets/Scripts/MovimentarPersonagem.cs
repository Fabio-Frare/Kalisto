using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPersonagem : MonoBehaviour
{
    public CharacterController controle;
    private Animator anim;

    public float velocidade = 3f;
    public float alturaPulo = 1.5f;
    public float gravidade = -40f;
 
    public Transform checaChao;
    public float raioEsfera = 0.9f;
    public LayerMask chaoMask;
    public bool estaNoChao;
    public bool parado;

    Vector3 velocidadeCai;

    void Start()
    {
        controle = GetComponent<CharacterController>();    
        anim = GetComponent<Animator>();    
    }

    void Update()
    {        
        movimentoPersonagem();  
    }

    void movimentoPersonagem()
    {
        // cria uma esfera de raioesfera na posição checaChao, batendo com a mascara no chão
        // se estah em contato com chaoMask, então retorna true.        
        estaNoChao = Physics.CheckSphere(checaChao.position, raioEsfera, chaoMask);

        float x = Input.GetAxis("Horizontal");       
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;
        //anim.SetInteger("transition", 0);

        //controle.Move(mover * velocidade * Time.deltaTime);   
        if(mover == Vector3.zero)
        {
            parado = true;
        } else 
        {
            parado = false;
        }
       
        if (parado && estaNoChao)
        {
            anim.SetInteger("transition", 0);
        }
       
        if (!parado)
        {   
            controle.Move(mover * velocidade * Time.deltaTime); 

            // Enquanto Shift estiver pressionado, aumenta a velocidade simulando uma corrida.
            if (estaNoChao && Input.GetKey(KeyCode.LeftShift))
            {
                velocidade = 6f;
                anim.SetInteger("transition", 2);
            } else
            {   
                velocidade = 3f;
                anim.SetInteger("transition", 1); 
            } 
        }    

        // SPACE => utilizado para simular pulo do heroi.
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
            {   
                velocidadeCai.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);  
                anim.SetInteger("transition", 3);                   
            }
 
        if (!estaNoChao)
        {
            velocidadeCai.y += gravidade * Time.deltaTime;
        }

        controle.Move(velocidadeCai * Time.deltaTime);
     
    }

   void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(checaChao.position, raioEsfera);
    }
    
    
}
