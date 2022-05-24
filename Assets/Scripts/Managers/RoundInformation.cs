using System.Collections;
using System.Collections.Generic;
using DataStructures;
using Extension;
using Projectiles;
using UnityEngine;

namespace Managers
{
    public class RoundInformation : MonoBehaviour
    {
        private Round _nextRound;
        private static RoundInformation _instance;
        private ActiveObjectsTracker aot;
        private static Economics eco;
        private Log _log;
        private int _currentRound;
        private HealthManager _manager;

        public static RoundInformation Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<RoundInformation>();
                }
                return _instance;
            }
        }


        //Well... Started out as a placeholder but ended up like this I'm so sorry
        private void Awake() {
            _log = FindObjectOfType<Log>();
            aot = FindObjectOfType<ActiveObjectsTracker>();
            eco = GameObject.FindGameObjectWithTag("EconomicsHandler").GetComponent<Economics>();
            if(eco == null) eco = Economics.Instance;
            _manager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HealthManager>();
            _manager.AddLogger();
        }
        
        public void RoundStart(int round) {
            StartCoroutine(Spawn(_nextRound));
            ConfigureRound(round);
            _log.Logger.Log(LogType.Log, $"{_currentRound};");
        }

        public static void RoundEnd() {

            foreach (Projectile p in FindObjectsOfType<Projectile>()) 
                p.ResetProjectileFromEnemy();
            
            eco.ReceiveIncome(100 + Instance._currentRound);
            Instance._log.Logger.Log(LogType.Log, $"{Instance._currentRound}; ; ; ; ; ; ; ; {eco.CumulativeMoney};");
            
            foreach (GameObject obj in FindObjectsOfType<GameObject>()) {
                ILoggable loggable;
                if ((loggable = obj.GetInterface<ILoggable>()) == null) continue;
                loggable.Log();
            }
        }

        private void ConfigureRound(int round) {
            Invoke("Round" +round, 0f);
            _currentRound = round-1;
        }

        private IEnumerator Spawn(Round round) {
            List<Wave> waves = round.Get;
            foreach (Wave t in waves) {
                StartCoroutine(round.SpawnWave(t));
                yield return new WaitForSeconds(t.TimeUntilNext);
            }
            aot.FullRoundSpawned();
        }

        private void Start() {
            //TestRound();
            Round1();
            //Round40();
        }

        private void TestRound() {
            _nextRound = new Round(new List<Wave> {
                //new Wave(15f, new List<BloonType> {new BloonType("Ceramic", 5, 1f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Purple", 20, 1.5f)})
            });
        }

        private Round NextRound(int round) {

            return null;
        }

        private void Round1() {
            _nextRound = new Round((new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Red", 20, 0.5f)})
            }));
        }

        private void Round2() {
            _nextRound = new Round((new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Red", 35, 0.4f)})
            }));
        }

        private void Round3() {
            _nextRound = new Round(new List<Wave> {
                new Wave(4f, new List<BloonType> { new BloonType("Red", 10, 0.4f)}),
                new Wave(0f,new List<BloonType>{new BloonType("Red", 15, 0.5f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Blue", 5, 0.3f)})
            });
        }

        private void Round4() {
            _nextRound = new Round(new List<Wave> {
                new Wave(4f, new List<BloonType> { new BloonType("Red", 20, 0.5f)}),
                new Wave(3.2f, new List<BloonType> { new BloonType("Blue", 8, 0.4f)}),
                new Wave(2f, new List<BloonType>{new BloonType("Blue", 10, 0.2f)}),
                new Wave(0f, new List<BloonType>{new BloonType("Red", 15, 0.4f)})
            });
        }

        private void Round5() {
            _nextRound = new Round(new List<Wave> {
                new Wave(1f, new List<BloonType> {new BloonType("Red", 10, 0.5f),}),
                new Wave(4f, new List<BloonType> {new BloonType("Blue", 13, 0.4f),}),
                new Wave(0f, new List<BloonType> {new BloonType("Blue", 14, 0.3f),})
            });
        }

        private void Round6() {
            _nextRound = new Round(new List<Wave> {
                new Wave(3f, new List<BloonType> {new BloonType("Red", 15, 0.5f),}),
                new Wave(2f, new List<BloonType> {new BloonType("Blue", 15, 0.4f),}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 4, 0.2f),})
            });
        }

        private void Round7() {
            _nextRound = new Round(new List<Wave> {
                new Wave(2f, new List<BloonType> {new BloonType("Red", 20, 0.6f),}),
                new Wave(3f, new List<BloonType> {new BloonType("Blue", 20, 0.4f),}), 
                new Wave(0f, new List<BloonType> {new BloonType("Green", 5, 0.2f),})
            });
        }

        private void Round8() {
            _nextRound = new Round(new List<Wave> {
                new Wave(2f, new List<BloonType> {new BloonType("Red", 10, 0.7f),}),
                new Wave(4f, new List<BloonType> {new BloonType("Blue", 20, 0.5f),}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 14, 0.2f),})
            });
        }

        private void Round9() {
            _nextRound = new Round(new List<Wave> {
                new Wave(3f, new List<BloonType> {new BloonType("Green", 10, 0.4f),}),
                new Wave(3f, new List<BloonType> {new BloonType("Green", 10, 0.3f),}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 10, 0.3f),})
            });
        }

        private void Round10() {
            _nextRound = new Round((new List<Wave> {
                new Wave(5f, new List<BloonType> {new BloonType("Blue", 25, 0.2f)}),
                new Wave(7f, new List<BloonType> {new BloonType("Blue", 27, 0.3f)}),
                new Wave(5f, new List<BloonType> {new BloonType("Blue", 25, 0.3f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Blue", 25, 0.2f)}),
            }));
        }

        private void Round11() {
            _nextRound = new Round((new List<Wave> {
                new Wave(1f, new List<BloonType> {new BloonType("Red", 6, 0.5f)}),
                new Wave(3f, new List<BloonType> {new BloonType("Blue", 12, 0.6f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Yellow", 15, 0.7f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 12, 0.3f)}),
            }));
        }
        
        private void Round12() {
            _nextRound = new Round((new List<Wave> {
                new Wave(1f, new List<BloonType> {new BloonType("Blue", 5, 0.7f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Blue", 5, 0.7f)}),
                new Wave(1f, new List<BloonType> {new BloonType("Yellow", 5, 0.6f)}),
                new Wave(1f, new List<BloonType> {new BloonType("Blue", 5, 0.7f)}),
                new Wave(3f, new List<BloonType> {new BloonType("Green", 7, 0.6f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 15, 0.6f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Yellow", 3, 0.8f)}),
            }));
        }
        
        private void Round13() {
            _nextRound = new Round((new List<Wave> {
                new Wave(1f, new List<BloonType> {new BloonType("Blue", 10, 0.7f)}),
                new Wave(6f, new List<BloonType> {new BloonType("Blue", 10, 0.7f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 7, 0.6f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Blue", 10, 1f)}),
                new Wave(4f, new List<BloonType> {new BloonType("Green", 6, 0.6f)}),
                new Wave(3f, new List<BloonType> {new BloonType("Blue", 10, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Blue", 10, 0.5f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 10, 0.6f)}),
            }));
        }
        
        private void Round14() {
            _nextRound = new Round((new List<Wave> {
                new Wave(3f, new List<BloonType> {new BloonType("Red", 30, 0.7f)}), 
                new Wave(2f, new List<BloonType> {new BloonType("Blue", 7, 1f)}),
                new Wave(5f, new List<BloonType> {new BloonType("Green", 10, 1.5f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Blue", 8, 0.2f)}),
                new Wave(2f, new List<BloonType> {new BloonType("Yellow", 9, 0.6f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Red", 19, 0.3f)}),
            }));
        }
        
        private void Round15() {
            _nextRound = new Round((new List<Wave> {
                new Wave(3f, new List<BloonType> {new BloonType("Blue", 20, 0.5f)}), 
                new Wave(2f, new List<BloonType> {new BloonType("Blue", 7, 1f)}),
                new Wave(4f, new List<BloonType> {new BloonType("Green", 10, 0.8f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 8, 0.2f)}),
                new Wave(2f, new List<BloonType> {new BloonType("Yellow", 13, 0.6f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Red", 10, 0.3f)}),
            }));
        }
        
        private void Round16() {
            _nextRound = new Round((new List<Wave> {
                new Wave(1f, new List<BloonType> {new BloonType("Red", 30, 0.2f)}), 
                new Wave(0f, new List<BloonType> {new BloonType("Blue", 15, 0.4f)}),
                new Wave(2f, new List<BloonType> {new BloonType("Pink", 7, 0.5f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 12, 0.7f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Yellow", 10, 0.5f)}),
            }));
        }
        
        private void Round17() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Green", 10, 0.7f)}),
                new Wave(4f, new List<BloonType> {new BloonType("Yellow", 4, 0.5f)}),
                new Wave(2f, new List<BloonType> {new BloonType("Green", 15, 0.6f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Yellow", 4, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 25, 0.4f)}),
            });
        }
        
        private void Round18() {
            _nextRound = new Round(new List<Wave> {
                new Wave(1.05f, new List<BloonType> {new BloonType("Green", 20, 0.7f)}),
                new Wave(10f, new List<BloonType> {new BloonType("Green", 20, 0.7f)}),
                new Wave(0.6f, new List<BloonType> {new BloonType("Green", 20, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 20, 0.4f)}),
            });
        }
        
        private void Round19() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Pink", 15, 0.8f)}),
                new Wave(6f, new List<BloonType> {new BloonType("Yellow", 20, 0.5f)}),
                new Wave(0.1f, new List<BloonType> {new BloonType("Green", 5, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green", 10, 0.4f)}),
            });
        }

        private void Round20() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Black", 6, 0.5f)}),
            });
        }
        
        private void Round21() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Yellow", 40, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Pink", 20, 0.6f)}),
            });
        }
        
        private void Round22() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("White", 16, 0.4f)}),
            });
        }
        
        private void Round23() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0.8f, new List<BloonType> {new BloonType("White", 7, 0.5f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Black", 7, 0.5f)}),
            });
        }
        
        private void Round24() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0.4f, new List<BloonType> {new BloonType("Yellow", 20, 0.5f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Green"/*(Camo)*/, 100, 0.3f)}),
            });
        }
        
        private void Round25() {
            _nextRound = new Round(new List<Wave> {
                new Wave(10f, new List<BloonType> {new BloonType("Yellow", 25, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Purple", 10, 0.5f)}),
            });
        }
        
        private void Round26() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0.5f, new List<BloonType> {new BloonType("White", 7, 0.5f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Black", 7, 0.5f)}),
            });
        }
        
        private void Round27() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0.5f, new List<BloonType> {new BloonType("Red", 50, 0.2f)}),
                new Wave(3f, new List<BloonType> {new BloonType("Blue", 100, 0.3f)}),
                new Wave(5f, new List<BloonType> {new BloonType("Green", 60, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Yellow", 45, 0.3f)}),
            });
        }
        
        private void Round28() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Lead", 6, 0.5f)}),
            });
        }
        
        private void Round29() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Pink", 65, 0.2f)}),
            });
        }
        
        private void Round30() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Lead", 9, 0.4f)}),
            });
        }
        
        private void Round31() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0.25f, new List<BloonType> {new BloonType("Black", 8, 0.5f)}),
                new Wave(3f, new List<BloonType> {new BloonType("White", 8, 0.5f)}),
                new Wave(2.25f, new List<BloonType> {new BloonType("Zebra", 8, 0.5f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Rainbow", 8, 0.5f)}),
            });
        }
        
        private void Round32() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0.35f, new List<BloonType> {new BloonType("Black", 15, 0.7f)}),
                new Wave(6f, new List<BloonType> {new BloonType("White", 20, 0.7f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Purple", 8, 0.5f)}),
            });
        }
        
        private void Round33() {
            _nextRound = new Round(new List<Wave> {
                new Wave(2f, new List<BloonType> {new BloonType("Purple"/*Camo*/, 20, 0.7f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Pink"/*Camo*/, 30, 0.5f)}),
            });
        }
        
        private void Round34() {
            _nextRound = new Round(new List<Wave> {
                new Wave(40f, new List<BloonType> {new BloonType("Yellow", 160, 0.3f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Zebra", 13, 0.6f)}),
            });
        }
        
        private void Round35() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Pink", 35, 0.4f)}),
                new Wave(3.25f, new List<BloonType> {new BloonType("Black", 30, 0.5f)}),
                new Wave(2.25f, new List<BloonType> {new BloonType("White", 25, 0.5f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Rainbow", 5, 0.5f)}),
            });
        }
        
        private void Round36() {
            _nextRound = new Round(new List<Wave> {
                new Wave(40f, new List<BloonType> {new BloonType("Pink", 100, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Zebra"/*Camo*/, 15, 0.5f)}),
            });
        }
        
        private void Round37() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("Black", 25, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("White", 25, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("White"/*Camo*/, 7, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Zebra", 10, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Lead", 15, 0.4f)}),
            });
        }
        
        private void Round38() {
            _nextRound = new Round(new List<Wave> {
                new Wave(4f, new List<BloonType> {new BloonType("Pink", 42, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("White", 17, 0.4f)}),
                new Wave(6f, new List<BloonType> {new BloonType("Zebra", 10, 0.4f)}),
                new Wave(3f, new List<BloonType> {new BloonType("Lead", 15, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Ceramic", 2, 0.4f)}),
            });
        }
        
        private void Round39() {
            _nextRound = new Round(new List<Wave> {
                new Wave(4f, new List<BloonType> {new BloonType("Black", 10, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("White", 10, 0.4f)}),
                new Wave(6f, new List<BloonType> {new BloonType("Zebra", 20, 0.4f)}),
                new Wave(3f, new List<BloonType> {new BloonType("Rainbow", 18, 0.4f)}),
                new Wave(0f, new List<BloonType> {new BloonType("Rainbow"/*Regrowth*/, 2, 0.4f)}),
            });
        }
        
        private void Round40() {
            _nextRound = new Round(new List<Wave> {
                new Wave(0f, new List<BloonType> {new BloonType("BossTemp", 1, 1f)}),
            });
        }

        private void Round41() {
            
        }
        
    }
}
