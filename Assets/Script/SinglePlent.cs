using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SinglePlent : MonoBehaviour {

    public GameObject[] SingleXing;//每一颗球体引用
    public Text[] JieShao;//场景中的两个文本
	public AudioClip Sound;//声音资源
	// Use this for initialization
	void Start () {
        
	}
    public void BackToMain()
    {
		if(Constraints.YinXiao=="open")//添加音效
		{
			Vector3 CameraPos=Camera.main.transform.position;
			AudioSource.PlayClipAtPoint(Sound,CameraPos);//设置播放片段的位置，离摄像机越近越清晰
		}
        Application.LoadLevelAsync("SkyboxBlueNebula_Scene");//切换场景
    }
	void Update () {
		/*选中某颗天体之后显示详细介绍内容*/
        if (Constraints.NameStar == "Sun")
        {
            JieShao[0].text = "太阳";
            JieShao[1].text = "太阳是太阳系中唯一的恒星和会发光的天体，是太阳系的中心天体，太阳系质量的99.86%都集中在太阳。太阳系中的八大行星、小行星、流星、彗星、外海王星天体以及星际尘埃等，都围绕着太阳运行（公转）。而太阳则围绕着银河系的中心运行，也就是公转。";
            SingleXing[8].SetActive(true);
            SingleXing[9].SetActive(true);
        }
        else if (Constraints.NameStar == "shuixing1")
        {
            JieShao[0].text = "水星";
            JieShao[1].text = "水星是太阳系八大行星最内侧也是最小的一颗行星，中国称为辰星，有着八大行星中最大的轨道偏心率。它每87.968个地球日绕行太阳一周，而每公转2.01周同时也自转3圈。水星有着太阳系行星中最小的轨道倾角。";
            SingleXing[0].SetActive(true);
            
        }
        else if (Constraints.NameStar == "jinxing2")
        {
            JieShao[0].text = "金星";
            JieShao[1].text = "金星是太阳系中八大行星之一，按离太阳由近及远的次序，是第二颗，距离太阳0.725天文单位。它是离地球最近的行星。中国古代称之为长庚、启明、太白或太白金星，罗马神话中称作维纳斯，希腊神话中称为阿佛洛狄特。公转周期是224.71地球日。";
            SingleXing[1].SetActive(true);
        }
		else if (Constraints.NameStar == "diqiu3")
        {
            JieShao[0].text = "地球";
            JieShao[1].text = "地球是太阳系八大行星之一，按离太阳由近及远的次序排为第三颗。它有一个天然卫星—月球，二者组成一个天体系统—地月系统。地球作为一个行星，远在46亿年以前起源于原始太阳星云。地球会与外层空间的其他天体相互作用，包括太阳和月球。";

            SingleXing[2].SetActive(true);
        }
        else if (Constraints.NameStar == "huoxing4")
        {
            JieShao[0].text = "火星";
            JieShao[1].text = "火星是太阳系八大行星之一，是太阳系由内往外数的第四颗行星。直径约为地球的一半，自转轴倾角、自转周期均与地球相近，公转一周约为地球公转时间的两倍。在西方称为“战神玛尔斯”，中国则称为“荧惑”。橘红色外表是因为地表的赤铁矿（氧化铁）。";
            SingleXing[3].SetActive(true);
        }
        else if (Constraints.NameStar == "muxing5")
        {
            JieShao[0].text = "木星";
            JieShao[1].text = "木星是太阳系从内向外的第五颗行星，亦为太阳系中体积最大、自转最快的行星。它的质量为太阳的千分之一，但为太阳系中其他行星质量总和的2.5倍。木星与土星、天王星、海王星皆属气体行星，因此四者又合称类木行星，亦为太阳系体积最大、自转最快的行星。";
            SingleXing[4].SetActive(true);
        }
        else if (Constraints.NameStar == "tuxing6")
        {
            JieShao[0].text = "土星";
            JieShao[1].text = "土星，为太阳系八大行星之一，至太阳距离（由近到远）位于第六、体积则仅次于木星。并与木星、天王星及海王星同属气体（类木）巨星。古代中国亦称之镇星或填星。土星有土星环，截止2012年已发现62颗卫星。";
            SingleXing[5].SetActive(true);
        }
        else if (Constraints.NameStar == "tianwangxing7")
        {
            JieShao[0].text = "天王星";
            JieShao[1].text = "天王星是太阳系由内向外的第七颗行星，其体积在太阳系中排名第三(比海王星大)，质量排名第四(小于海王星)。天王星大气的主要成分是氢和氦，还包含较高比例的由水、氨、甲烷等结成的“冰”，与可以探测到的碳氢化合物。";
            SingleXing[6].SetActive(true);
        }
        else if (Constraints.NameStar == "haiwangxing8")
        {
            JieShao[0].text = "海王星";
            JieShao[1].text = "海王星是远日行星之一，按照同太阳的平均距离由近及远排列，为第八颗行星。它的亮度仅为7.85等，只有在天文望远镜里才能看到它。由于它那荧荧的淡蓝色光，所以西方人用罗马神话中的海神—“尼普顿”的名字来称呼它。在中文里，把它译为海王星。";
            SingleXing[7].SetActive(true);
        }
	}
}
