using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPersonagem : MonoBehaviour
{
    public CharacterController controle;
    public float velocidade = 6f;
    public float alturaPulo = 1f;
    public float gravidade = -40f;
 
    public Transform checaChao;
    public float raioEsfera = 0.9f;
    public LayerMask chaoMask;
    public bool estaNoChao;

    Vector3 velocidadeCai;

    void Start()
    {
        controle = GetComponent<CharacterController>();        
    }

    void Update()
    {
        // cria uma esfera de raioesfera na posição checaChao, batendo com a mascara no chão
        // se estah em contato com chaoMask, então retorna true.        
        estaNoChao = Physics.CheckSphere(checaChao.position, raioEsfera, chaoMask);

        float x = Input.GetAxis("Horizontal");       
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;

        controle.Move(mover * velocidade * Time.deltaTime);    

        
        if (estaNoChao && Input.GetButtonDown("Jump"))
        {
            velocidadeCai.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }   

        if (!estaNoChao)
        {
            velocidadeCai.y += gravidade * Time.deltaTime;
        }

        controle.Move(velocidadeCai * Time.deltaTime);

        // Enquanto Shift estiver pressionado, dobra a velocidade simulando uma corrida.
        if (estaNoChao && Input.GetKey(KeyCode.LeftShift))
        {
            velocidade = 12f;
        } else
        {
            velocidade = 6f;
        }
       
    }

   void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(checaChao.position, raioEsfera);
    }
    
    
}
