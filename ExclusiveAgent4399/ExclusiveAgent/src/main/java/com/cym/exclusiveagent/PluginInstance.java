package com.cym.exclusiveagent;

import android.app.Activity;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import cn.m4399.ea.api.ExclusiveAgent;
import cn.m4399.ea.api.UseActivityCodeResult;
import cn.m4399.ea.api.UseGiftCodeResult;
import cn.m4399.ea.spi.OnInitGlobalListener;
import cn.m4399.ea.spi.OnInvokeListener;

public class PluginInstance {

    private static Activity unityActivity;

    private static int activeCode=-1;
    private static boolean isInited = false;
    private static  String gameObjectName;

    public static void Init(Activity activity,String gamekey,String goName)
    {
        unityActivity = activity;
        gameObjectName = goName;
        // 初始化 SDK
        ExclusiveAgent.init(unityActivity, gamekey, false, new OnInitGlobalListener() {

            // 激活状态回调函数
            @Override
            public void onActivationState(int code, String msg) {
                // code-> 0: 激活功能未配置
                // code-> 1: 激活成功
                // code-> 2: 已激活成功过
                activeCode = code;
                Toast.makeText(unityActivity, msg, Toast.LENGTH_SHORT)
                        .show();

                // TODO: 游戏方业务逻辑
                JSONObject pms=new JSONObject();
                try {
                    //参数只能参一个，所以包装成json对象
                    pms.putOpt("code",code);
                    pms.putOpt("msg",msg);
                } catch (JSONException e) {
                    e.printStackTrace();
                }
                UnityPlayer.UnitySendMessage(gameObjectName,"OnActivationState4399",pms.toString());
            }

            // SDK 初始化成功回调函数
            @Override
            public void onInitComplete() {
                // TODO: 游戏方业务逻辑
                isInited=true;
                // TODO: 游戏方业务逻辑
                UnityPlayer.UnitySendMessage(gameObjectName,"OnInitComplete4399","");
            }
        });
    }
    public  static void OpenGameHub()
    {
        // 检查游戏圈是否启用，游戏可以根据此结果决定是否展示“游戏圈”入口
        if(ExclusiveAgent.isGameHubEnabled()){
        // 打开游戏圈
            ExclusiveAgent.openGameHub(unityActivity);
        }
    }

    public static  void OpenActivity(String activityID)
    {
        // 检查活动是否启用以及是否有活动，游戏可以根据此结果决定是否显示“活动”入口
        if (ExclusiveAgent.isActivityEnabled() && ExclusiveAgent.isHasActivity()) {
            // 打开活动页，SDK 根据运营策略可能会打开游戏盒或内置网页
            ExclusiveAgent.openActivityDetailById(unityActivity, activityID);
        }
    }

    public static void UseActivityCode(String activityID,String code)
    {
        /**
         使用活动激活码
         your_activity_id: 活动ID
         your_activity_code: 激活码
         **/
        ExclusiveAgent.useActivityCode(activityID, code, new OnInvokeListener<UseActivityCodeResult>() {
            @Override
            public void onExecuted(Exception e, UseActivityCodeResult result) {
                if (e != null) {
                    // TODO: 处理异常情况
                    UnityPlayer.UnitySendMessage(gameObjectName,"OnUseActivityCodeError4399","");
                    return;
                }

                // TODO: 根据开发者需要写入相应的业务代码
                /**
                 * UseActivityCodeResult 字段包含:
                 *   result: boolean 是否使用成功
                 *   message: string 详细描述信息
                 */
                JSONObject pms=new JSONObject();
                try {
                    //参数只能参一个，所以包装成json对象
                    pms.putOpt("result",result.result);
                    pms.putOpt("message",result.message);
                } catch (JSONException e2) {
                    e2.printStackTrace();
                }
                UnityPlayer.UnitySendMessage(gameObjectName,"OnUseActivityCode4399",pms.toString());
            }
        });
    }
    public static void OpenGift(String giftID)
    {
        // 检查礼包是否开启以及是否有礼包，游戏可以根据此结果决定是否展示“礼包”入口
        if (ExclusiveAgent.isGiftEnabled() && ExclusiveAgent.isHasGift()) {
            // 打开礼包页，SDK 根据运营策略可能会打开游戏盒或内置网页
            ExclusiveAgent.openGiftDetailById(unityActivity, giftID);
        }
    }
    public static void UseGiftCode(String giftID,String code)
    {
        /**
         使用礼包激活码
         your_gift_id: 活动ID
         your_gift_code: 激活码
         **/
        ExclusiveAgent.useGiftCode(giftID, code, new OnInvokeListener<UseGiftCodeResult>() {
            @Override
            public void onExecuted(Exception e, UseGiftCodeResult result) {
                if (e != null) {
                    // TODO: 异常处理
                    UnityPlayer.UnitySendMessage(gameObjectName,"OnUseGiftCodeError4399","");
                    return;
                }

                // TODO: 根据开发者需要写入相应的业务代码
                /**
                 * UseGiftCodeResult 字段包含:
                 *   result: boolean 是否使用成功
                 *   message: string 详细描述信息
                 */
                JSONObject pms=new JSONObject();
                try {
                    //参数只能参一个，所以包装成json对象
                    pms.putOpt("result",result.result);
                    pms.putOpt("message",result.message);
                } catch (JSONException e2) {
                    e2.printStackTrace();
                }
                UnityPlayer.UnitySendMessage(gameObjectName,"OnUseGiftCode4399",pms.toString());
            }
        });
    }

    public static int GetActiveCode()
    {
        return activeCode;
    }
    public static boolean GetInited()
    {
        return isInited;
    }


}
