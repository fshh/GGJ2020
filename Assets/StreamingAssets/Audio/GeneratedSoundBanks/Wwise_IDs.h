/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID ROCK_WINS = 171267552U;
        static const AkUniqueID SET_STATE_GAMEPLAY = 2289732960U;
        static const AkUniqueID SET_STATE_MENU = 2302270969U;
        static const AkUniqueID SET_STATE_ROCK_WINNING = 1928154282U;
        static const AkUniqueID SET_STATE_SYNTH_WINNING = 3921242951U;
        static const AkUniqueID START_GAME = 1114964412U;
        static const AkUniqueID SYNTH_WINS = 1405444765U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace FIGHT_SONG_STATE_GROUP
        {
            static const AkUniqueID GROUP = 2321468137U;

            namespace STATE
            {
                static const AkUniqueID GAMEPLAY_STATE = 762757699U;
                static const AkUniqueID MENU_STATE = 3941853002U;
            } // namespace STATE
        } // namespace FIGHT_SONG_STATE_GROUP

        namespace GAMEPLAY_STATE_GROUP
        {
            static const AkUniqueID GROUP = 3663545511U;

            namespace STATE
            {
                static const AkUniqueID NEUTRAL_STATE = 2379433868U;
                static const AkUniqueID ROCK_GROUP_WINNING_STATE = 3581880889U;
                static const AkUniqueID ROCK_SOLO_STATE = 141569174U;
                static const AkUniqueID SYNTH_GROUP_WINNING_STATE = 2603661118U;
                static const AkUniqueID SYNTH_SOLO_STATE = 4010060711U;
            } // namespace STATE
        } // namespace GAMEPLAY_STATE_GROUP

        namespace MENU_STATE_GROUP
        {
            static const AkUniqueID GROUP = 444966094U;

            namespace STATE
            {
                static const AkUniqueID MENU = 2607556080U;
            } // namespace STATE
        } // namespace MENU_STATE_GROUP

    } // namespace STATES

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID FIGHT_SONG_SOUNDBANK = 616830705U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
