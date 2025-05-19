#if (IL2CPPMELON)
using S1Vehicles = Il2CppScheduleOne.Vehicles;
using Il2CppScheduleOne.DevUtilities;
using Il2Cpp;
using Il2CppFishNet;
using Il2CppFishNet.Connection;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Vehicles = ScheduleOne.Vehicles;
using ScheduleOne.DevUtilities;
using FishNet;
using FishNet.Connection;
#endif
using System.Reflection;
using UnityEngine;
using S1API.Logging;

namespace S1API.Vehicles
{
    /// <summary>
    /// Represents a land vehicle in the game.
    /// </summary>
    public class LandVehicle
    {
        /// <summary>
        /// Logger for the LandVehicle class.
        /// </summary>
        private static readonly Log Logger = new Log("S1API.LandVehicle");

        /// <summary>
        /// INTERNAL: The stored reference to the land vehicle in-game (see <see cref="S1Vehicles.LandVehicle"/>).
        /// </summary>
        internal S1Vehicles.LandVehicle S1LandVehicle = null!;

        /// <summary>
        /// The stored reference to protected vehiclePrice field in the land vehicle in-game.
        /// </summary>
        private static readonly FieldInfo? VehiclePriceField =
            typeof(S1Vehicles.LandVehicle).GetField("vehiclePrice", BindingFlags.NonPublic);

        /// <summary>
        /// Connection to the player that owns the vehicle.
        /// </summary>
        private NetworkConnection? _conn;

        /// <summary>
        /// Creates a new LandVehicle instance.
        /// </summary>
        public LandVehicle(string vehicleCode)
        {
            var vehiclePrefab = NetworkSingleton<S1Vehicles.VehicleManager>.Instance.GetVehiclePrefab(vehicleCode);
            if (vehiclePrefab == null)
            {
                Logger.Error($"SpawnVehicle: '{vehicleCode}' is not a valid vehicle code!");
                return;
            }

            var component = UnityEngine.Object.Instantiate<GameObject>(vehiclePrefab.gameObject)
                .GetComponent<S1Vehicles.LandVehicle>();

            component.SetGUID(GUIDManager.GenerateUniqueGUID());
            NetworkSingleton<S1Vehicles.VehicleManager>.Instance.AllVehicles.Add(component);

            S1LandVehicle = component;
            _setConnection();
        }

        /// <summary>
        /// INTERNAL: Creates a LandVehicle instance from an in-game land vehicle instance.
        /// </summary>
        /// <param name="landVehicle">The in-game land vehicle instance.</param>
        internal LandVehicle(S1Vehicles.LandVehicle landVehicle)
        {
            S1LandVehicle = landVehicle;
            _setConnection();
        }

        /// <summary>
        /// Sets the connection to the player that owns the vehicle.
        /// </summary>
        private void _setConnection()
        {
            var nm = InstanceFinder.NetworkManager;
            if (nm.IsClientOnly)
            {
                var tempConn = InstanceFinder.ClientManager.Connection;
                if (tempConn != null && tempConn.IsValid)
                    _conn = tempConn;
            }
            else if (nm.IsServerOnly || (nm.IsServer && !nm.IsClient))
            {
                var owner = S1LandVehicle.Owner;
                if (owner != null && owner.IsValid)
                    _conn = owner;
            }
        }

        /// <summary>
        /// Vehicle price.
        /// </summary>
        public float VehiclePrice
        {
            get => S1LandVehicle.VehiclePrice;
            set => VehiclePriceField?.SetValue(S1LandVehicle, value);
        }

        /// <summary>
        /// Vehicle's top speed.
        /// </summary>
        public float TopSpeed
        {
            get => S1LandVehicle.TopSpeed;
            set => S1LandVehicle.TopSpeed = value;
        }

        /// <summary>
        /// If the vehicle is owned by the player.
        /// </summary>
        public bool IsPlayerOwned
        {
            get => S1LandVehicle.IsPlayerOwned;
            set => _setIsPlayerOwned(value);
        }

        /// <summary>
        /// Helper method to set the vehicle as player owned.
        /// </summary>
        /// <param name="isPlayerOwned">If true, sets vehicle as player owned</param>
        private void _setIsPlayerOwned(bool isPlayerOwned)
        {
            S1LandVehicle.SetIsPlayerOwned(_conn, isPlayerOwned);
            // make sure to add/remove the vehicle from the player owned vehicles list
            if (isPlayerOwned)
                NetworkSingleton<S1Vehicles.VehicleManager>.Instance.PlayerOwnedVehicles.Add(S1LandVehicle);
            else
                NetworkSingleton<S1Vehicles.VehicleManager>.Instance.PlayerOwnedVehicles.Remove(S1LandVehicle);
        }

        /// <summary>
        /// Vehicle's color.
        /// </summary>
        public VehicleColor Color
        {
            get => (VehicleColor)S1LandVehicle.OwnedColor;
            set => _setColor(value);
        }

        /// <summary>
        /// Helper method to set the vehicle color.
        /// </summary>
        /// <param name="color">Vehicle's color</param>
        private void _setColor(VehicleColor color)
        {
            var setOwnedColorMethod =
                typeof(S1Vehicles.LandVehicle).GetMethod("SetOwnedColor",
                    BindingFlags.Instance | BindingFlags.NonPublic);
            if (setOwnedColorMethod == null)
            {
                Logger.Error("SetOwnedColor method not found!");
                return;
            }

            setOwnedColorMethod.Invoke(S1LandVehicle, [_conn, (S1Vehicles.Modification.EVehicleColor)color]);
        }

        /// <summary>
        /// Spawns the vehicle in the game world.
        /// </summary>
        /// <param name="position">Position in the world</param>
        /// <param name="rotation">Rotation of the vehicle</param>
        public void Spawn(Vector3 position, Quaternion rotation)
        {
            if (!InstanceFinder.IsServer)
            {
                Logger.Warning("Spawn can only be called on the server!");
                return;
            }
            
            S1LandVehicle.transform.position = position;
            S1LandVehicle.transform.rotation = rotation;
            NetworkSingleton<S1Vehicles.VehicleManager>.Instance.Spawn(S1LandVehicle.gameObject);
        }
    }
}