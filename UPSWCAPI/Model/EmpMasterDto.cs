namespace UPSWCAPI.Model
{
    // Employee Registration
    public class EmpMasterDto
    {
        // keys used by stored proc
        public string? ECODE { get; set; }
        public string? LoginUser { get; set; }

        // Category
        public int? EmployementId { get; set; }
        public int? WTypeId { get; set; }

        // Personal
        public string? EMP_NAME { get; set; }
        public string? FATH_NAME { get; set; }
        public DateTime? DATE_OF_BIRTH { get; set; }      // SP: DATETIME2(0)
        public string? QUALIFICATION { get; set; }
        public string? SEX { get; set; }                 // SP: CHAR(1) — send single character (e.g. "M")
        public string? MARITAL_STATUS { get; set; }      // SP: CHAR(1) — send single char (e.g. "M"/"S"/"O")
        public string? LOC_ADD1 { get; set; }
        public string? LOC_ADD2 { get; set; }
        public string? LOC_ADD3 { get; set; }
        public int? LOC_PIN { get; set; }                // SP: NUMERIC(6,0)
        public string? LOC_STATE { get; set; }
        public string? PAR_ADD1 { get; set; }
        public string? PAR_ADD2 { get; set; }
        public string? PAR_ADD3 { get; set; }
        public int? PAR_PIN { get; set; }                // SP: NUMERIC(6,0)
        public string? PAR_STATE { get; set; }
        public string? HOME_TOWN { get; set; }
        public int? NO_OF_CHILD { get; set; }
        public string? WIFE_GOVT_SER_FLAG { get; set; }  // SP: CHAR(1) 'Y'/'N'
        public string? BIRTH_PLACE { get; set; }
        public string? UNIV { get; set; }
        public string? QALIFIC1 { get; set; }
        public string? QALIFIC2 { get; set; }
        public string? QALIFIC3 { get; set; }
        public string? QALIFIC4 { get; set; }
        public string? Q_STATUS { get; set; }
        public string? RES_PHONE { get; set; }
        public string? WIFE_SER_FLAG { get; set; }       // SP: CHAR(1) 'Y'/'N'
        public string? WIFE_ECODE { get; set; }         // SP: VARCHAR(10)
        public string? PHONE_NO { get; set; }
        public string? EMAIL { get; set; }
        public int? ReligionId { get; set; }
        public string? Photo { get; set; }
        public int? BloodGroupId { get; set; }
        public string? AdharNo { get; set; }

        // Department
        public string? GRP { get; set; }
        public int? SECTIONID { get; set; }
        public string? DEPTT_CODE { get; set; }
        public int? DESIGID { get; set; }
        public string? DESIG_CODE { get; set; }
        public string? PF_NO { get; set; }
        public int? PROMOTION { get; set; }
        public string? ORDER_NO { get; set; }
        public string? RECFLAG { get; set; }
        public int? DEPTTID { get; set; }
        public string? DEP_TYPE { get; set; }
        public DateTime? DATE_OF_JOIN { get; set; }
        public DateTime? CONFIRM_DATE { get; set; }
        public DateTime? RETIRE_DATE { get; set; }
        public int? CategoryId { get; set; }

        // Account
        public int? BANKID { get; set; }
        public string? ACCT_NO { get; set; }
        public string? GOVT_ACC_FLAG { get; set; }
        public string? ACCO_CODE { get; set; }
        public string? BANK_CODE { get; set; }
        public string? PAN_NO { get; set; }             // SP: VARCHAR(20)

        // Salary
        public int? PAY_GRADE { get; set; }
        public decimal? BASIC { get; set; }
        public decimal? PERSONAL_PAY { get; set; }
        public decimal? SPECIAL_PAY { get; set; }
        public string? MODE_SAL_PAY { get; set; }       // SP: CHAR(1)
        public decimal? GRADE_PAY { get; set; }
        public string? PAY_RELEASE_FLAG { get; set; }
        public string? BGT_CAT_CODE { get; set; }

        // Allowances
        public string? OFF_VECH_FLAG { get; set; }
        public string? HANDICAPT_FLAG { get; set; }
        public string? NURSING_FLAG { get; set; }
        public string? PAT_CARE_FLAG { get; set; }
        public string? UNIFORM_FLAG { get; set; }
        public string? WASHING_FLAG { get; set; }
        public string? TRAINING_FLAG { get; set; }
        public string? MEDI_DED_FLAG { get; set; }
        public string? NEWS_FLAG { get; set; }
        public decimal? MEDI_AMT { get; set; }

        // Deductions
        public decimal? GPF_DED { get; set; }
        public decimal? CDT_AMT { get; set; }
        public decimal? INCOME_TAX { get; set; }
        public int? BUS_KM { get; set; }
        public decimal? GIS_DED { get; set; }

        // Pension
        public string? PRAN_NO { get; set; }
        public DateTime? GPF_FINAL_DT { get; set; }
        public string? PENSION_TYPE { get; set; }
        public decimal? COMMUT_BSK { get; set; }
        public DateTime? PENS_ORDER_DT { get; set; }
        public decimal? PPO_NO { get; set; }            // SP: NUMERIC(10,0)
        public DateTime? DT_OF_PENSION { get; set; }
        public string? PERSONAL_IDENTITY { get; set; }
        public string? HIGHT_OF_PENSION { get; set; }

        // Other
        public string? REMARK { get; set; }
        public string? IMP_MSG { get; set; }
        public string? NO_DUES { get; set; }
        public string? NO_DUES_REMARK { get; set; }
    }

    //Payslip

    public class PayslipDto
    {
        public string ECODE { get; set; }
        public int YR_NO { get; set; }
        public int MTH_NO { get; set; }
        public string? EMP_NAME { get; set; }
        public string? PAN_NO { get; set; }
        public string? BGT_CAT_CODE { get; set; }
        public string? BANK_AC { get; set; }
        public DateTime? DATE_OF_JOIN { get; set; }
        public string? GPF_NO { get; set; }
        public string? PAY_GRADE { get; set; }
        public string? DEPTT { get; set; }
        public string? DEPTT_DESC { get; set; }
        public string? DESIG_CODE { get; set; }
        public string? DESIG_DESC { get; set; }
        public string? GIS_NO { get; set; }
        public string? LEVEL_CODE { get; set; }
        public decimal? BASIC_SAL { get; set; }
        public decimal? GRADE_PAY { get; set; }
        public string? PRAN_NO { get; set; }
        //public decimal? NET_SAL { get; set; }
        public string? GROUPTY { get; set; }

        // Allowances
        public decimal? DP { get; set; }
        public decimal? CCA { get; set; }
        public decimal? SPL_PAY { get; set; }
        public decimal? WASHING { get; set; }
        public decimal? LEAVE_ENCASH { get; set; }
        public decimal? NPA { get; set; }
        public decimal? DA_ON_NPA { get; set; }
        public decimal? TA { get; set; }
        public decimal? DA_ON_TA { get; set; }
        public decimal? DA { get; set; }
        public decimal? DEP_ALW { get; set; }
        public decimal? HRA { get; set; }
        public decimal? BOOK_ALW { get; set; }
        public decimal? TRAINING { get; set; }
        public decimal? AcdAllow { get; set; }
        public decimal? NURSING { get; set; }
        public decimal? UNIFORM { get; set; }
        public decimal? NEWS_ALW { get; set; }
        public decimal? OTH_ALW1 { get; set; }
        public decimal? OTH_ALW2 { get; set; }
        public decimal? GROSS_EARN { get; set; }
        public decimal? PAT_CARE { get; set; }

        // Deductions
        public decimal? GPF_DED { get; set; }
        public decimal? GPF_ADV_DED { get; set; }
        public decimal? GIS_DED { get; set; }
        public decimal? NPS_DED { get; set; }
        public decimal? NPS_BACK { get; set; }
        public decimal? MEDI_DED { get; set; }
        public decimal? RENT_DED { get; set; }
        public decimal? WATER_ELEC_DED { get; set; }
        public decimal? WATER_DED { get; set; }
        public decimal? TELE_PH_DED { get; set; }
        public decimal? RELIEF_FUND_DED { get; set; }
        public decimal? IT_TAX_DED { get; set; }
        public decimal? HBA_DED { get; set; }
        public decimal? CREDIT_SAL_ADV_DED { get; set; }
        public decimal? SALARY_RECOVERY { get; set; }
        public decimal? FST_ADV_DED { get; set; }
        public decimal? BUS_DED { get; set; }
        public decimal? CAR_ADV_DED { get; set; }
        public decimal? BENEVOLENT_DED { get; set; }
        public decimal? NPSArrear { get; set; }
        public decimal? ElectricityCharg { get; set; }
        public decimal? MCARecovery { get; set; }
        public decimal? NPSCurrent { get; set; }
        public decimal? TransportDedSelf { get; set; }
        public decimal? TransportDedDep { get; set; }
        public decimal? TransportDedRefund { get; set; }
        public decimal? BUS_REC { get; set; }
        public decimal? BUS_DEP_DED { get; set; }
        public decimal? GrossDed { get; set; }
        public decimal? NET_SAL { get; set; }
        public string? scalePay { get; set; }
        public string? SalDesc { get; set; }
        public string? MonthYear
        {
            get
            {
                if (YR_NO > 0 && MTH_NO > 0)
                {
                    return new DateTime(YR_NO, MTH_NO, 1).ToString("MMM-yyyy");
                }
                else if (ECODE == "TOTAL")
                {
                    return "TOTAL";
                }
                return null;
            }
        }
    }

    public class PayslipRequestDto
    {
        public int ProcId { get; set; } = 1; // default to GET PAYSLIP
        public string? ECODE { get; set; }
        public int StartYR { get; set; }
        public int StartMTH { get; set; }
        public int EndYR { get; set; }
        public int EndMTH { get; set; }
    }

    public class EmployeeInfo
    {
        public string? EMP_NAME { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
    }

}
