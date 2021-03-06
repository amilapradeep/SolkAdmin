
IF OBJECT_ID('uspDailyEnquiryForAdmin', 'P') IS NOT NULL
	DROP PROCEDURE [dbo].[uspDailyEnquiryForAdmin]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Amila Pradeep
-- Create date: 2018-Apr-20
-- Description:	Get daily enquiry for sending Quote by Admin

-- @SRStatus NULL = All
--			1 = Open
--			2 = Closed
--			3 = Expired
-- =============================================
CREATE PROCEDURE [dbo].[uspDailyEnquiryForAdmin] 
	@Id bigint = null,
	@ipv_RequestNoOrVehicleNo varchar(50) = NULL, 
	@SRStatus int = 1,
	@FromDate datetime = NULL, 
	@ToDate datetime = NULL,
	@bIsOnlyNotSentQuote bit = 0
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @RequestNoOrVehicleNo VARCHAR(20) = @ipv_RequestNoOrVehicleNo;
	DECLARE @TimeToExpire decimal(18,2), 
			@QuotationExpire decimal(18,2)

	SELECT @TimeToExpire = CONVERT(decimal(18,2), Value) FROM Settings WHERE [Key] = 'REQUEST_EXPIRE_WINDOW'
	SELECT @ToDate = ISNULL(@ToDate, DATEADD(dd, 1, getdate())),  @FromDate = ISNULL(@FromDate, '2016-01-01')
	SELECT @FromDate = DATEADD(dd, -1, @FromDate)

	SELECT
		SR.Id,
		SR.Code, 
		CASE  SR.[Status]
		  WHEN 1 THEN 'Initial'   
		  WHEN 2 THEN 'Pending Response'
		  WHEN 3 THEN 'Seller Responded' 
		  WHEN 4 THEN 'Expired' 
		  WHEN 5 THEN 'Closed'    
		END AS EnquiryStatus,
		U.UserName AS UserPhoneNumber,
		ISNULL(UP.FirstName, U.Name) AS FirstName,
		UP.LastName,
		CASE UP.Gender WHEN 0 THEN 'Male' WHEN 1 THEN 'Female' ELSE 'Unspecified' END AS Gender,
		SR.VehicleNo,
		dbo.fnVehicleRegistrationType(SR.RegistrationCategory) AS RegistrationCategory,
		CAST(SR.VehicleValue AS NVARCHAR(100)) AS VehicleValue,
		dbo.fnClaimType(SR.InsuranceTypeId) AS ClaimType,
		dbo.fnVehicleUsageType(SR.UsageType) AS UsageType,
		SR.VehicleYear,
		CASE SR.IsFinanced WHEN 1 THEN 'Yes' ELSE 'No' END AS IsFinanced,
		SR.Location,
		CONVERT(nvarchar(16), dbo.fnGetSlLocalTime(SR.TimeOccured), 121) AS CreatedTime,
		CONVERT(nvarchar(16), dbo.fnGetSlLocalTime(DATEADD(dd, @TimeToExpire, SR.TimeOccured)), 121) AS ExpireTime,
		--CASE WHEN EXISTS(
		--					SELECT 1 FROM [dbo].[RequestQuotations] RQ INNER JOIN [Notifications] N ON RQ.Id = N.[RecordId] AND [Type] = 5
		--					WHERE RQ.[ServiceRequestId] = SR.Id AND N.RecieverId = SR.UserId
		--				) THEN 'Yes' ELSE 'No' END AS FollowupNotification,
		--(SELECT TOP 1 CONVERT(nvarchar(16),dbo.fnGetSlLocalTime([Time]), 121) 
		--	FROM [dbo].[RequestQuotations] RQ INNER JOIN [Notifications] N ON RQ.Id = N.[RecordId] AND [Type] = 5
		--	WHERE RQ.[ServiceRequestId] = SR.Id AND N.RecieverId = SR.UserId 
		--	ORDER BY N.Id DESC) AS FollowUpNotificationTime,
		UP.Street AS StreetName,
		UP.City,
		UP.Phone AS Telephone,
		UP.Email,
		dbo.fnContactMethod(UP.ContactMethod) AS ContactMethod,
		dbo.fnRequestOrigin(SR.ClientType) AS RequestOrigin,
		'No' AS IsQuoteSent,
		NULL AS LastQuoteSentDate
	FROM ServiceRequests SR
		INNER JOIN dbo.Users U ON SR.UserId = U.Id
		LEFT JOIN dbo.UserProfiles UP ON U.Id = UP.UserId
	WHERE (@Id IS NULL OR (SR.Id = @Id)) AND
		(@RequestNoOrVehicleNo IS NULL OR (SR.Code LIKE '%' + @RequestNoOrVehicleNo + '%' OR SR.VehicleNo LIKE '%' + @RequestNoOrVehicleNo + '%')) AND			
			CASE WHEN @SRStatus IS NULL THEN 1
				WHEN @SRStatus = 1 AND SR.[Status] IN (1,2,3) THEN 1
				WHEN @SRStatus = 2 AND SR.[Status] = 5 THEN 1
				WHEN @SRStatus = 3 AND SR.[Status] = 4 THEN 1 END = 1
			AND SR.TimeOccured BETWEEN  @FromDate AND @ToDate

END


