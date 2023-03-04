using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public class ObservableTriggerSample : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;

    private ReactiveCollection<GameObject> _generatedCubeList = new ReactiveCollection<GameObject>();

    private float forceStrength = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        SetTriggerEvent();
    }

    private void SetTriggerEvent()
    {
        //マウスクリックイベントの作成
        var mouseButtonDownEvent = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
        
        //マウスクリックイベントが発生したらクリック位置を取得しCubeの作成
        mouseButtonDownEvent
            .Select(_ => Input.mousePosition)
            .Subscribe(pos => GenerateCube(pos));

        //ReactiveCollectionなキューブのリストを監視し、新たな追加イベントなどがあればそのキューブの衝突イベントに対する処理を登録する
        _generatedCubeList
            .ObserveAdd()
            .Select(x => x.Value)
            .Subscribe(addedCube =>
            {
                RegisterCollisionEvent(addedCube);
                RegisterClickEvent(addedCube);
            });
    }

    //Cubeを生成することだけに専念する関数
    //ここの内部で衝突時のイベント登録処理を直接記述するのはもちろんのこと衝突イベント登録を行う関数を呼び出すことも避けたい
    //のであくまでGenerateCubeが行うのgeneratedCubeListへの追加までにとどめ、それを検知して別で衝突時のイベントなどを登録する！
    private void GenerateCube(Vector3 mousePos)
    {
        mousePos.z = -Camera.main.transform.position.z;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        var cube = Instantiate(cubePrefab, mouseWorldPos, Quaternion.identity);
        _generatedCubeList.Add(cube);
    }

    //生成されたキューブの監視リストへの追加イベントを検知して、衝突のイベントの登録を行う
    private void RegisterCollisionEvent(GameObject cube)
    {
        var rigidBody = cube.gameObject.GetComponent<Rigidbody>();
        var audioSource = cube.gameObject.GetComponent<AudioSource>();
        
        //キューブの衝突イベント全てに対して効果音の再生を行う
        cube.OnCollisionEnterAsObservable()
            .Subscribe(_ => audioSource.Play());
        
        //キューブの衝突イベントに対して、地面との衝突か？というフィルタリングをしてそうであれば上方向への力をかける
        cube.OnCollisionEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag("Ground"))
            .Subscribe(_ => rigidBody.AddForce(Vector3.up * forceStrength, ForceMode.Impulse));
        
        //キューブの衝突イベントに対して、キューブとの衝突か？というフィルタリングをしてそうであれば削除する
        cube.OnCollisionEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag("Cube"))
            .Subscribe(_ => Destroy(cube));
    }
    
    //生成されたキューブの監視リストへの追加イベントを検知して、クリック時のイベントの登録を行う
    private void RegisterClickEvent(GameObject cube)
    {
        var meshRenderer = cube.gameObject.GetComponent<MeshRenderer>();
        
        //キューブをクリックスリベントに対して、色をランダムに変える
        cube.OnMouseEnterAsObservable()
            .Subscribe(_ => meshRenderer.material.color = new Color(Random.value,Random.value,Random.value));
    }
}
