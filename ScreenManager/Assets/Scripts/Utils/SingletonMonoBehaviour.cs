using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

	private static T _instance;
    private static object _lock = new object();
 
	public static T Instance {
		
		get {
			lock(_lock) {
				if (_instance == null) {
					Debug.LogErrorFormat("(Singleton) No instance of type {0} found.", typeof(T).ToString());
				}
 
				return _instance;
			}
		}
	}

	protected virtual void Awake() {
        if (_instance != null) {
            Debug.LogErrorFormat("(Singleton) Instance of type {0} already exists.", typeof(T).ToString());
        } else {
            _instance = this as T;
        }
    }
}