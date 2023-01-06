using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unibill;
using Unibill.Impl;
using Uniject.Impl;
using UnityEngine;

public class Unibiller
{
	[CompilerGenerated]
	private sealed class _003C_internal_hook_events_003Ec__AnonStoreyB
	{
		internal BillerFactory factory;

		internal Biller biller;

		private static Action<string, string> _003C_003Ef__am_0024cache2;

		internal void _003C_003Em__14(bool success)
		{
			if (Unibiller.onBillerReady == null)
			{
				return;
			}
			if (success)
			{
				Unibiller.downloadManager = factory.instantiateDownloadManager(biller);
				DownloadManager downloadManager = Unibiller.downloadManager;
				if (_003C_003Ef__am_0024cache2 == null)
				{
					_003C_003Ef__am_0024cache2 = _003C_003Em__15;
				}
				downloadManager.onDownloadCompletedEvent += _003C_003Ef__am_0024cache2;
				Unibiller.downloadManager.onDownloadCompletedEvent += Unibiller.onDownloadCompletedEventString;
				Unibiller.downloadManager.onDownloadFailedEvent += Unibiller.onDownloadFailedEvent;
				Unibiller.downloadManager.onDownloadProgressedEvent += Unibiller.onDownloadProgressedEvent;
				Unibiller.onBillerReady((biller.State != BillerState.INITIALISED) ? UnibillState.SUCCESS_WITH_ERRORS : UnibillState.SUCCESS);
			}
			else
			{
				Unibiller.onBillerReady(UnibillState.CRITICAL_ERROR);
			}
		}

		private static void _003C_003Em__15(string item, string path)
		{
			if (Unibiller.onDownloadCompletedEvent != null)
			{
				Unibiller.onDownloadCompletedEvent(item, new DirectoryInfo(path));
			}
		}
	}

	private static Biller biller;

	private static DownloadManager downloadManager;

	private static DownloadManager DownloadManager;

	public static BillingPlatform BillingPlatform
	{
		get
		{
			if (biller != null)
			{
				return biller.InventoryDatabase.CurrentPlatform;
			}
			return BillingPlatform.UnityEditor;
		}
	}

	public static bool Initialised
	{
		get
		{
			if (biller != null)
			{
				return biller.State == BillerState.INITIALISED || biller.State == BillerState.INITIALISED_WITH_ERROR;
			}
			return false;
		}
	}

	public static UnibillError[] Errors
	{
		get
		{
			if (biller != null)
			{
				return biller.Errors.ToArray();
			}
			return new UnibillError[0];
		}
	}

	public static PurchasableItem[] AllPurchasableItems
	{
		get
		{
			return biller.InventoryDatabase.AllPurchasableItems.ToArray();
		}
	}

	public static PurchasableItem[] AllNonConsumablePurchasableItems
	{
		get
		{
			return biller.InventoryDatabase.AllNonConsumablePurchasableItems.ToArray();
		}
	}

	public static PurchasableItem[] AllConsumablePurchasableItems
	{
		get
		{
			return biller.InventoryDatabase.AllConsumablePurchasableItems.ToArray();
		}
	}

	public static PurchasableItem[] AllSubscriptions
	{
		get
		{
			return biller.InventoryDatabase.AllSubscriptions.ToArray();
		}
	}

	public static string[] AllCurrencies
	{
		get
		{
			return biller.CurrencyIdentifiers;
		}
	}

	public static event Action<UnibillState> onBillerReady;

	public static event Action<PurchasableItem> onPurchaseCancelled;

	public static event Action<PurchaseEvent> onPurchaseCompleteEvent;

	public static event Action<PurchasableItem> onPurchaseComplete;

	public static event Action<PurchasableItem> onPurchaseFailed;

	public static event Action<PurchasableItem> onPurchaseDeferred;

	public static event Action<PurchasableItem> onPurchaseRefunded;

	public static event Action<string, DirectoryInfo> onDownloadCompletedEvent;

	public static event Action<string, string> onDownloadCompletedEventString;

	public static event Action<string, int> onDownloadProgressedEvent;

	public static event Action<string, string> onDownloadFailedEvent;

	public static event Action<bool> onTransactionsRestored;

	public static void Initialise(List<ProductDefinition> runtimeProducts = null)
	{
		if (biller == null)
		{
			RemoteConfigManager remoteConfigManager = new RemoteConfigManager(new UnityResourceLoader(), new UnityPlayerPrefsStorage(), new UnityLogger(), Application.platform, runtimeProducts);
			UnibillConfiguration config = remoteConfigManager.Config;
			GameObject gameObject = new GameObject();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			UnityUtil util = gameObject.AddComponent<UnityUtil>();
			BillerFactory billerFactory = new BillerFactory(new UnityResourceLoader(), new UnityLogger(), new UnityPlayerPrefsStorage(), new RawBillingPlatformProvider(config), config, util);
			biller = billerFactory.instantiate();
			_internal_hook_events(biller, billerFactory);
		}
		biller.Initialise();
	}

	public static PurchasableItem GetPurchasableItemById(string unibillPurchasableId)
	{
		if (biller != null)
		{
			return biller.InventoryDatabase.getItemById(unibillPurchasableId);
		}
		return null;
	}

	public static void initiatePurchase(PurchasableItem purchasable, string developerPayload = "")
	{
		if (biller != null)
		{
			biller.purchase(purchasable, developerPayload);
		}
	}

	public static void initiatePurchase(string purchasableId, string developerPayload = "")
	{
		if (biller != null)
		{
			biller.purchase(purchasableId, developerPayload);
		}
	}

	public static int GetPurchaseCount(PurchasableItem item)
	{
		if (biller != null)
		{
			return biller.getPurchaseHistory(item);
		}
		return 0;
	}

	public static int GetPurchaseCount(string purchasableId)
	{
		if (biller != null)
		{
			return biller.getPurchaseHistory(purchasableId);
		}
		return 0;
	}

	public static decimal GetCurrencyBalance(string currencyIdentifier)
	{
		if (biller != null)
		{
			return biller.getCurrencyBalance(currencyIdentifier);
		}
		return 0m;
	}

	public static void CreditBalance(string currencyIdentifier, decimal amount)
	{
		if (biller != null)
		{
			biller.creditCurrencyBalance(currencyIdentifier, amount);
		}
	}

	public static bool DebitBalance(string currencyIdentifier, decimal amount)
	{
		if (biller != null)
		{
			return biller.debitCurrencyBalance(currencyIdentifier, amount);
		}
		return false;
	}

	public static void restoreTransactions()
	{
		if (biller != null)
		{
			biller.restoreTransactions();
		}
	}

	public static void clearTransactions()
	{
		if (biller != null)
		{
			biller.ClearPurchases();
		}
	}

	public static void DownloadContent(string bundleId, PurchasableItem proofOfPurchase = null)
	{
		if (downloadManager == null)
		{
			return;
		}
		string receipt = string.Empty;
		if (proofOfPurchase != null)
		{
			if (GetPurchaseCount(proofOfPurchase) == 0 && Unibiller.onDownloadFailedEvent != null)
			{
				Unibiller.onDownloadFailedEvent(bundleId, "Proof of purchase is not owned!");
				return;
			}
			receipt = proofOfPurchase.receipt;
		}
		downloadManager.downloadContentFor(bundleId, receipt);
	}

	public static DirectoryInfo GetDownloadableContentFor(PurchasableItem item)
	{
		if (downloadManager != null && item.hasDownloadableContent)
		{
			return new DirectoryInfo(downloadManager.getContentPath(item.downloadableContentId));
		}
		return null;
	}

	public static string GetDownloadableContentPathFor(string bundleId)
	{
		if (downloadManager != null)
		{
			return downloadManager.getContentPath(bundleId);
		}
		return null;
	}

	public static bool IsContentDownloaded(string bundleId)
	{
		if (downloadManager != null)
		{
			return downloadManager.isDownloaded(bundleId);
		}
		return false;
	}

	public static bool IsDownloadScheduled(string bundleId)
	{
		if (downloadManager != null)
		{
			return downloadManager.isDownloadScheduled(bundleId);
		}
		return false;
	}

	public static void DeleteDownloadedContent(string bundleId)
	{
		if (downloadManager != null)
		{
			downloadManager.deleteContent(bundleId);
		}
	}

	public static void _internal_hook_events(Biller biller, BillerFactory factory)
	{
		_003C_internal_hook_events_003Ec__AnonStoreyB _003C_internal_hook_events_003Ec__AnonStoreyB = new _003C_internal_hook_events_003Ec__AnonStoreyB();
		_003C_internal_hook_events_003Ec__AnonStoreyB.factory = factory;
		_003C_internal_hook_events_003Ec__AnonStoreyB.biller = biller;
		_003C_internal_hook_events_003Ec__AnonStoreyB.biller.onBillerReady += _003C_internal_hook_events_003Ec__AnonStoreyB._003C_003Em__14;
		_003C_internal_hook_events_003Ec__AnonStoreyB.biller.onPurchaseCancelled += _onPurchaseCancelled;
		_003C_internal_hook_events_003Ec__AnonStoreyB.biller.onPurchaseComplete += _onPurchaseComplete;
		_003C_internal_hook_events_003Ec__AnonStoreyB.biller.onPurchaseFailed += _onPurchaseFailed;
		_003C_internal_hook_events_003Ec__AnonStoreyB.biller.onPurchaseDeferred += _onPurchaseDeferred;
		_003C_internal_hook_events_003Ec__AnonStoreyB.biller.onPurchaseRefunded += _onPurchaseRefunded;
		_003C_internal_hook_events_003Ec__AnonStoreyB.biller.onTransactionsRestored += _onTransactionsRestored;
	}

	private static void _onPurchaseCancelled(PurchasableItem item)
	{
		if (Unibiller.onPurchaseCancelled != null)
		{
			Unibiller.onPurchaseCancelled(item);
		}
	}

	private static void _onPurchaseComplete(PurchaseEvent e)
	{
		if (Unibiller.onPurchaseComplete != null)
		{
			Unibiller.onPurchaseComplete(e.PurchasedItem);
		}
		if (Unibiller.onPurchaseCompleteEvent != null)
		{
			Unibiller.onPurchaseCompleteEvent(e);
		}
	}

	private static void _onPurchaseFailed(PurchasableItem item)
	{
		if (Unibiller.onPurchaseFailed != null)
		{
			Unibiller.onPurchaseFailed(item);
		}
	}

	private static void _onPurchaseDeferred(PurchasableItem item)
	{
		if (Unibiller.onPurchaseDeferred != null)
		{
			Unibiller.onPurchaseDeferred(item);
		}
	}

	private static void _onPurchaseRefunded(PurchasableItem item)
	{
		if (Unibiller.onPurchaseRefunded != null)
		{
			Unibiller.onPurchaseRefunded(item);
		}
	}

	private static void _onTransactionsRestored(bool success)
	{
		if (Unibiller.onTransactionsRestored != null)
		{
			Unibiller.onTransactionsRestored(success);
		}
	}
}
