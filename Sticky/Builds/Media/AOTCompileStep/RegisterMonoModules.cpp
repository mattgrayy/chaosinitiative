
#if defined(TARGET_IPHONE_SIMULATOR) && TARGET_IPHONE_SIMULATOR
    #define DECL_USER_FUNC(f) void f() __attribute__((weak_import))
    #define REGISTER_USER_FUNC(f)\
        do {\
        if(f != NULL)\
            mono_dl_register_symbol(#f, (void*)f);\
        else\
            ::printf_console("Symbol '%s' not found. Maybe missing implementation for Simulator?\n", #f);\
        }while(0)
#else
    #define DECL_USER_FUNC(f) void f() 
    #if !defined(__arm64__)
    #define REGISTER_USER_FUNC(f) mono_dl_register_symbol(#f, (void*)&f)
    #else
        #define REGISTER_USER_FUNC(f)
    #endif
#endif
extern "C"
{
    typedef void* gpointer;
    typedef int gboolean;
    void                mono_aot_register_module(gpointer *aot_info);
#if __ORBIS__ || SN_TARGET_PSP2
#define DLL_EXPORT __declspec(dllexport)
#else
#define DLL_EXPORT
#endif
#if !(TARGET_IPHONE_SIMULATOR)
    extern gboolean     mono_aot_only;
    extern gpointer*    mono_aot_module_Assembly_CSharp_info; // Assembly-CSharp.dll
    extern gpointer*    mono_aot_module_Mono_Posix_info; // Mono.Posix.dll
    extern gpointer*    mono_aot_module_Mono_Security_info; // Mono.Security.dll
    extern gpointer*    mono_aot_module_mscorlib_info; // mscorlib.dll
    extern gpointer*    mono_aot_module_System_Configuration_info; // System.Configuration.dll
    extern gpointer*    mono_aot_module_System_Core_info; // System.Core.dll
    extern gpointer*    mono_aot_module_System_info; // System.dll
    extern gpointer*    mono_aot_module_System_Security_info; // System.Security.dll
    extern gpointer*    mono_aot_module_System_Xml_info; // System.Xml.dll
    extern gpointer*    mono_aot_module_UnityEngine_info; // UnityEngine.dll
    extern gpointer*    mono_aot_module_UnityEngine_Networking_info; // UnityEngine.Networking.dll
    extern gpointer*    mono_aot_module_UnityEngine_UI_info; // UnityEngine.UI.dll
#endif // !(TARGET_IPHONE_SIMULATOR)
}
DLL_EXPORT void RegisterMonoModules()
{
#if !(TARGET_IPHONE_SIMULATOR) && !defined(__arm64__)
    mono_aot_only = true;
    mono_aot_register_module(mono_aot_module_Assembly_CSharp_info);
    mono_aot_register_module(mono_aot_module_Mono_Posix_info);
    mono_aot_register_module(mono_aot_module_Mono_Security_info);
    mono_aot_register_module(mono_aot_module_mscorlib_info);
    mono_aot_register_module(mono_aot_module_System_Configuration_info);
    mono_aot_register_module(mono_aot_module_System_Core_info);
    mono_aot_register_module(mono_aot_module_System_info);
    mono_aot_register_module(mono_aot_module_System_Security_info);
    mono_aot_register_module(mono_aot_module_System_Xml_info);
    mono_aot_register_module(mono_aot_module_UnityEngine_info);
    mono_aot_register_module(mono_aot_module_UnityEngine_Networking_info);
    mono_aot_register_module(mono_aot_module_UnityEngine_UI_info);
#endif // !(TARGET_IPHONE_SIMULATOR) && !defined(__arm64__)

}

