using System;
using System.Text;
using Android.App;
using DiarioProducao.Classes.Tools;


namespace DiarioProducao.Classes.ProducaoObservacao
{

  public class ListaProducaoObservacao
  {

    public AdapterProducaoObservacao ListaProducaoObservacaoAdapter;

    public ListaProducaoObservacao ( Activity activityMestre, AcessoSql acessoSql, Int64 numSerieLmp )
    {
      var filtro = new StringBuilder ( );
      filtro.Clear ( );
      filtro.AppendFormat ( Sql.FiltroProducao, numSerieLmp );
      ListaProducaoObservacaoAdapter = new AdapterProducaoObservacao ( activityMestre, acessoSql, filtro.ToString ( ) );
    }

  }
}