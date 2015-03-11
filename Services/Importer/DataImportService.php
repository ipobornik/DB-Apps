<?php
namespace ANSR\Services\Importer;

/**
 * Class DataImportService
 * @package ANSR\Services\Importer
 * @author Ivan Yonkov <ivanynkv@gmail.com>
 */
class DataImportService
{

    /**
     * @var \ANSR\Services\Importer\ImportStrategy\ImportStrategy
     */
    private $importStrategy;

    /**
     * @var mixed
     */
    private $data;

    /**
     * @param $dbContext
     */
    public function __construct($dbContext)
    {

    }

    /**
     * @param ImportStrategy\ImportStrategy $strategy
     * @return $this
     */
    public function setImportStrategy(ImportStrategy\ImportStrategy $strategy)
    {
        $this->importStrategy = $strategy;

        return $this;
    }

    /**
     * @return ImportStrategy\ImportStrategy
     */
    public function getImportStrategy()
    {
        return $this->importStrategy;
    }

    /**
     * @param mixed $data
     * @return $this
     */
    public function setData($data)
    {
        $this->data = $data;

        return $this;
    }

    public function getData()
    {
        return $this->data;
    }

    /**
     * @return void
     */
    public function kickstart()
    {
        $this->getImportStrategy()->import($this->getData());
    }

    /**
     * @param string $dbType
     * @param string $dataType
     * @param mixed $data
     * @return self
     * @throws \Exception
     */
    public static function create($dbType, $dataType, $data)
    {
        $strategy = ImportStrategy\ImportStrategyFactory::create($dataType);

        return (new self($dbType))
            ->setData($data)
            ->setImportStrategy($strategy);
    }

}