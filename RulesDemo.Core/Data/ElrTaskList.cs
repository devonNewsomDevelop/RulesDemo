namespace RulesDemo.Core
{
    using System;

    namespace Data
    {
        public class ElrTaskList
        {
            public EpiCase? EpiCase { get; set; }
            public int IdElr { get; set; }
            public int? IdElrOrder { get; set; }
            public int? IdElrRequest { get; set; }
            public int IdElrObservation { get; set; }
            public string NmLast { get; set; }
            public string NmFirst { get; set; }
            public string NmMiddle { get; set; }
            public DateTime? DtBirth { get; set; }
            public string CdCounty { get; set; }
            public string CdIcd9 { get; set; }
            public string DsAccession { get; set; }
            public string CdElrStatus { get; set; }
            public byte? InDoiflag { get; set; }
            public string CdRob { get; set; }
            public string CdLabTest { get; set; }
            public string DsResult { get; set; }
            public string DsResultAlternate { get; set; }
            public string DsUnits { get; set; }
            public string DsResultComparator { get; set; }
            public string DsResultNumber1 { get; set; }
            public string DsResultSeparator { get; set; }
            public string DsResultNumber2 { get; set; }
            public string NmObservation { get; set; }
            public string NmObservationAlternate { get; set; }
            public string DsLoinc { get; set; }
            public string DsMethodology { get; set; }
            public string CdMerlinSpecimen { get; set; }
            public string CdSpecimen { get; set; }
            public string DsFluHeader { get; set; }
            public byte? InPickupStatus { get; set; }
            public string CdProcessDisposition { get; set; }
            public string CdOrderStatus { get; set; }
            public string NmProvider { get; set; }
            public DateTime? DtReported { get; set; }
            public DateTime? DtObservation { get; set; }
            public DateTime DtAdded { get; set; }
            public string DsParentResult { get; set; }
            public long? IdSsn { get; set; }
            public string CdGender { get; set; }
            public string CdSuffix { get; set; }
            public string CdRace { get; set; }
            public string CdEthnicity { get; set; }
            public string DsAdd1 { get; set; }
            public string DsAdd2 { get; set; }
            public string DsCity { get; set; }
            public string CdState { get; set; }
            public string DsZip { get; set; }
            public DateTime? DtElrInserted { get; set; }
            public string CdElrSpecimen { get; set; }
            public string DsElrSpecimen { get; set; }
            public DateTime? DtCollected { get; set; }
            public DateTime? DtReceived { get; set; }
            public string CdSendingFac { get; set; }
            public string DsSendingApp { get; set; }
            public string IdOrdProvSentinel { get; set; }
            public string IdOrdProv { get; set; }
            public string DsOrdProvAdd1 { get; set; }
            public string DsOrdProvAdd2 { get; set; }
            public string DsOrdProvCity { get; set; }
            public string CdOrdProvState { get; set; }
            public string DsOrdProvZip { get; set; }
            public string CdOrdProvCounty { get; set; }
            public string IdOrdFac { get; set; }
            public string NmFacility { get; set; }
            public string DsOrdFacAdd1 { get; set; }
            public string DsOrdFacAdd2 { get; set; }
            public string DsOrdFacCity { get; set; }
            public string CdOrdFacState { get; set; }
            public string DsOrdFacZip { get; set; }
            public string CdOrdFacCounty { get; set; }
            public string DsRefRange { get; set; }
            public string DsRefRange2 { get; set; }
            public string DsResultType { get; set; }
            public string DsAbnormalFlag { get; set; }
            public string CdAlternateResult { get; set; }
            public int? IdEvent { get; set; }
            public string CdResult { get; set; }
            public bool? InParent { get; set; }
            public int? IdFamily { get; set; }
            public string DsPhone { get; set; }
            public string InPregnant { get; set; }
            public string FilterAssignedMerlinResult { get; set; }
            public string DsBadReason { get; set; }
            public string IdPatient { get; set; }
            public string IdChanged { get; set; }
            public DateTime? DtChanged { get; set; }
            public string CdObxType { get; set; }
            public string CdObxAlt { get; set; }
            public string CdObxTypeAlt { get; set; }
            public string IdSub { get; set; }
            public string DsPlacernumber { get; set; }
            public string DsFillernumber { get; set; }
            public string DsParentplacernum { get; set; }
            public string DsParentfillernum { get; set; }
            public string InAntibiogram { get; set; }
            public double? AmLatitude { get; set; }
            public double? AmLongitude { get; set; }
            public bool? InAddressValidated { get; set; }
            public string DsAccumailMsg { get; set; }
            public int? IdProfile { get; set; }
            public int? IdProfileMaster { get; set; }
            public int? AmEditDistance { get; set; }
            public string IdStateno { get; set; }
            public int? IdFuzzyMatchLog { get; set; }
            public string CdAutoScenario { get; set; }
            public int? IdAutoScenario { get; set; }
            public string CdAmrIsolate { get; set; }
            public string DsPhoneIdOrdFac { get; set; }
            public string CdPerformingFacility { get; set; }
            public string DsPerformingFacility { get; set; }
            public string DsEmail { get; set; }
            public string CdObservationAlternate { get; set; }
            public string IdPerfFac { get; set; }
            public string IdSendFac { get; set; }
            public decimal? AmNumericResult { get; set; }
            public string DsDebug { get; set; }
            public int? IdLaboratory { get; set; }
            public int? IdProvider { get; set; }
            public int? IdFacility { get; set; }
            public string SSN { get; set; }

            public byte[] IdPerson { get; set; }

            public string NumericExpression
            {
                get
                {
                    return $"{DsResultComparator ?? string.Empty} "
                        + $"{DsResultNumber1 ?? string.Empty}"
                        + $"{DsResultSeparator ?? string.Empty}"
                        + $"{DsResultNumber2 ?? string.Empty}"
                        + $" {DsUnits ?? string.Empty}";
                }
            }
        }
    }

}