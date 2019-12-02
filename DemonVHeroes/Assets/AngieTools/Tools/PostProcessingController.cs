namespace AngieTools.Tools
{
    /*public class PostProcessingController : MonoBehaviour
    {
        private static PostProcessingController m_instance;

        private PostProcessVolume m_volume;

        private Vignette m_vignet = null;

        [SerializeField]
        private float m_intensity = 0.4f;


        private void Awake()
        {
            BindInstance();
            BindComponents();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        private void BindInstance()
        {
            if (m_instance == null)
            {
                m_instance = this;
                Debug.Log("Wave Binded");
            }
            else
            {
                Debug.LogError("Instance Already Bound Please Check: " + gameObject.name + " For Duplicate");
            }
        }


        private void BindComponents()
        {
            m_volume = GetComponent<PostProcessVolume>();
            m_volume.profile.TryGetSettings<Vignette>(out m_vignet);
        }

        public void BlurCamera()
        {
            m_vignet.intensity.value = m_intensity;

        }

        public void ResetCamera()
        {
            m_vignet.intensity.value = 0;
        }

        public static PostProcessingController Controller
        {
            get { return m_instance; }
        }
    }*/
}