namespace UPSWCAPI.Model
{
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
        public string? SECTION { get; set; }
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
}
