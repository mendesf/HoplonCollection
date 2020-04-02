using System.Collections.Generic;

namespace Hoplon.Collections
{

    /// <summary>
    /// O proposito desta interface é definir uma API para abstrair os detalhes técnicos de uma 
    /// colecao de alta performance para adicionar, remover e procurar um elemento, 
    /// considerando que a colecao possa ter um grande quantidade de elementos.
    /// Exemplo de organização da estrutura
    /// 
    ///  ___________________________________________________
    /// | chave          | subIndice | set                  |
    /// |---------------------------------------------------|
    /// |                | 1982      | arnaldo, joao, pedro |
    /// | ano.nascimento |----------------------------------|    
    /// |                | 1983      | bruno, maria         |
    /// |---------------------------------------------------|
    /// | cidades.sc     | 2000      | florianopolis, lages |
    /// |___________________________________________________|
    ///   Tabela 1
    /// 
    /// </summary>
    public interface IHoplonCollection
    {
        /// <summary>
        /// Adiciona um elemento na respectiva coleção representada pela chave e seu
        /// subIndice. 
        /// Os elementos de uma coleção representada por uma chave e subIndice são
        /// armazenados na memória em ordem crescente.
        /// Os elementos de uma coleção contidos na chave e subIndice são únicos (funciona
        /// como um Set).
        ///** Caso esta API seja chamada com um valor que já foi mapeado para a chave e
        /// para um subIndice, o valor antigo deve ser removido e o novo 
        /// valor deve ser adicionado na posição correta considerando ordem crescente.
        /// Exemplo: considerando a tabela acima, se adicionado o elemento "arnaldo" para a
        /// chave "ano.nascimento" com o subIndice "1983", a tabela ficaria assim:
        ///  _____________________________________________________
        /// | chave          | subIndice | set                    |
        /// |-----------------------------------------------------|
        /// |                | 1982      | joao, pedro            |
        /// | ano.nascimento |------------------------------------|    
        /// |                | 1983      | arnaldo, bruno,  maria |
        /// |-----------------------------------------------------|
        /// | cidades.sc     | 2000      | florianopolis, lages   |
        /// |_____________________________________________________|
        ///   Tabela 2
        ///   
        /// </summary>
        /// <param name="key">a chave que mapeia o item</param>
        /// <param name="subIndex">um indice para agrupar os valores</param>
        /// <param name="value"></param>
        /// <returns>Retorna true se o elemento foi adicionado, caso o elemento está presente 
        /// na lista retorna false e autaliza a posição de acordo com a ordem 
        /// crescente</returns>
        bool Add(string key, int subIndex, string value);

        /// <summary>
        /// Retorna uma lista com os valores que a chave esta armazenando de acordo com os
        /// limites de inicio e fim. 
        /// A lista retornada esta ordenada em ordem crescente considerando a ordem do
        /// subIndice.
        /// O parâmetro start e end são indices no formato zero-base, onde o primeiro elemento
        /// é representado pelo indice 0, o segundo elemento com 
        /// indice 1 e assim por diante.
        /// O parâmetro start e end representam um range inclusivo, ou seja, se for requisitado
        /// start=1 e end=3, será retornado uma lista com três 
        /// elementos, com o segundo elemento, terceiro e quarto elemento da coleção
        /// solicitada.
        /// O parâmetro end pode ter valores negativos, neste caso ele funciona como um offset
        /// considerando o útimo elemento. Exemplo: 
        ///    "-1" significa que o filtro deve retornar até o último elemento. Se o start é "0" e o
        /// end é "-1" significa q todos os elementos do chave serão retornados.
        ///    "-2" significa que o filtro deve retornar até o penúltimo elemento.
        /// Outros exemplos considerando a Tabela 1:
        ///    Get("ano.nascimento", 0, 3) será retornado os elementos: "arnaldo", "joao",
        ///  "pedro", "bruno" nesta ordem.
        ///    Get("ano.nascimento", 3, 3) será retornado o elemento: "bruno".
        /// Caso o parâmetro start seja menor que zero, deve ser considerado como se fosse o
        /// primeiro elemento.
        /// Caso o parâmetro end seja maior que o numero de elementos, deve ser considerado
        /// como se fosse o último elemento.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IList<string> Get(string key, int start, int end);

        /// <summary>
        /// Remove a chave com seus respectivos valores da coleção.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Retorna true se a chave foi removida, false caso a chave nao 
        /// exista</returns>
        bool Remove(string key);


        /// <summary>
        /// Remove todos os valores associados com uma chave e um subIndex
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subIndex"></param>
        /// <returns>Retorna true se a chave tem um subIndex associado e ele foi removido, 
        /// false caso contrario</returns>
        bool RemoveValuesFromSubIndex(string key, int subIndex);

        /// <summary>
        /// Retorna o indice que o elemento (value) esta posicionado.
        /// Este indice inicia com 0 e vai até tamanho da respectiva coleção considerando a
        /// chave.
        /// Exemplo, considere a tabela abaixo:
        ///  ___________________________________________________
        /// | chave          | subIndice | set                  |
        /// |---------------------------------------------------|
        /// |                | 1982      | arnaldo, joao, pedro |
        /// | ano.nascimento |----------------------------------|    
        /// |                | 1983      | bruno,  maria        |
        /// |---------------------------------------------------|
        /// | cidades.sc     | 2000      | florianopolis, lages |
        /// |___________________________________________________|
        /// A chamada IndexOf("ano.nascimento", "bruno") vai retornar: 3
        /// A chamada IndexOf("ano.nascimento", "arnaldo") vai retornar: 0
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long IndexOf(string key, string value);
    }
}